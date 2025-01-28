using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Enums;
using Palermo.Models;

namespace Palermo.Services
{
    public class Game
    {

        public List<Player> Players { get; set; }
        public GamePhase CurrentPhase { get; set; }
        public int RoundCount { get; set; }
        public Dictionary<Player, RoleType> Roles { get; set; }
        public string Winner {  get; set; }


        /// <summary>
        /// Starts the main game loop, alternating between Day and Night phases.
        /// </summary>
        /// <param name="playersNum"></param>
        /// <param name="names"></param>
        public void Start(int playersNum, List<string> names)
        {
            AssignRoles(playersNum, names);
            CurrentPhase = GamePhase.Day;

            for (RoundCount = 0; RoundCount < 10; RoundCount++)
            {
                while (CurrentPhase == GamePhase.Day)
                {
                    ExecuteDayPhase();
                    var result = IsThereAWinnerYet();
                    if (result == true)
                    {
                        DisplayResults();
                    }
                    else
                    {
                        ExecuteNightPhase();
                    }
                }
            }
        }

        /// <summary>
        /// Handles all actions for the Night phase.
        /// </summary>
        public void ExecuteNightPhase()
        {
            CurrentPhase = GamePhase.Night;

        }

        /// <summary>
        /// Handles voting and discussions for the Day phase.
        /// </summary>
        public void ExecuteDayPhase()
        {
            CurrentPhase = GamePhase.Day;
        }

        /// <summary>
        /// Determines if the game has ended and which side has won.
        /// </summary>
        /// <returns>true if there is a winner and who won, and false if nobody has won yet</returns>
        public bool IsThereAWinnerYet()
        {
            if(Roles.Where(x => x.Value == RoleType.Mafia).Count() == 0)
            {
                Winner = "Villagers";
                return true;
            }
            else if(Roles.Where(x => x.Value == RoleType.Detective).Count() == 0)
            {
                Winner = "Mafia";
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Shows the final roles and outcome of the game.
        /// </summary>
        public void DisplayResults()
        {

            var results = $"Here are the final results: \r\n Total number of rounds:{RoundCount} \r\n Winner:{Winner}";
        }

        /// <summary>
        /// Assigns roles randomly
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="playerNames"></param>
        /// <returns></returns>
        public string AssignRoles(int numberOfPlayers, List<string> playerNames)
        {
            Utils utils = new Utils();
            var error = string.Empty;

            if (numberOfPlayers > 5)
            {
                var remainingPlayers = numberOfPlayers;

                var detective = new Detective();
                Players.Add(detective);
                remainingPlayers -= 1;

                for (int i = 0; i < 2; i++)
                {
                    var mafia = new Mafia();
                    Players.Add(mafia);
                    remainingPlayers -= 1;
                }
                for (int i = 0; i < remainingPlayers; i++)
                {
                    var citizen = new Citizen();
                    Players.Add(citizen);
                    remainingPlayers -= 1;
                }

                var shuffledListPlayers = utils.ShuffleList(Players);
                Random random = new Random();
                for (int i = 0; i < playerNames.Count; i++)
                {
                    var randomPlayerName = playerNames[i];
                    var randomPlayer = utils.GetRandomPlayer(shuffledListPlayers);
                    Roles.Add(randomPlayer, randomPlayer.Role);
                    randomPlayer.Name = randomPlayerName;
                    shuffledListPlayers.Remove(randomPlayer);
                }
                error = string.Empty;
                return error;
            }
            else
            {
                error = "The number of players should be at least 5";
                return error;
            }
        }
    }
}
