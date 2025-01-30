using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
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

        public List<Player> Players { get; set; } = [];
        public GamePhase CurrentPhase { get; set; }
        public int RoundCount { get; set; }
        public Dictionary<Player, RoleType> Roles { get; set; } = [];
        public string Winner {  get; set; }


        /// <summary>
        /// Starts the main game loop, alternating between Day and Night phases.
        /// </summary>
        /// <param name="playersNum"></param>
        /// <param name="names"></param>
        public void Start(int playersNumber, List<string> names)
        {
            AssignRoles(playersNumber, names);
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
            var remainingPlayers = numberOfPlayers;
            var shuffledListNames = utils.ShuffleList(playerNames);

            
            if (numberOfPlayers > 5)
            {
                for (int i = 0; i <= shuffledListNames.Count; i++)
                {
                    var name = shuffledListNames[i];
                    var detective = new Detective(name);
                    Players.Add(detective);
                    remainingPlayers -= 1;
                    shuffledListNames.Remove(name);

                    for (int x = 0; x < 2; x++)
                    {
                        var mafia = new Mafia(name);
                        Players.Add(mafia);
                        remainingPlayers -= 1;
                        shuffledListNames.Remove(name);
                    }

                    for(int y = 0; y < remainingPlayers + 1; y++)
                    {
                         var citizen = new Citizen(name);
                         Players.Add(citizen);
                         remainingPlayers -= 1;
                         shuffledListNames.Remove(name);
                    }
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
