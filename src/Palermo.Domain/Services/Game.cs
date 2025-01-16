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

namespace Palermo.Models
{
    internal class Game
    {

        public List<Player> Players { get; set; }
        public GamePhase CurrentPhase { get; set; }
        public int RoundCount { get; set; }
        public Dictionary<Player, RoleType> Roles { get; set; }
        public string Winner {  get; set; }

        public string InitializeGame(int numberOfPlayers, List<string> playerNames)
        {
            //Sets up the game by assigning roles randomly.
            Utils utils = new Utils();
            CurrentPhase = GamePhase.Day;
            var error = string.Empty;

            if (numberOfPlayers > 5)
            {
                var remainingPlayers = numberOfPlayers;
                var shuffledListPlayers = utils.ShuffleList(Players);

                //Picks a random player
                //Adds him to the list Roles and defines him as detective
                //Removes him from the shuffled list
                //Calculates remaining players
                var detectivePlayer = utils.GetRandomPlayer(shuffledListPlayers);
                Roles.Add(detectivePlayer, RoleType.Detective);
                shuffledListPlayers.Remove(detectivePlayer);
                remainingPlayers -= 1;

                //Defines the mafia players
                for (int i = 0; i < 2; i++)
                {
                    var mafiaPlayer = utils.GetRandomPlayer(shuffledListPlayers);
                    Roles.Add(mafiaPlayer, RoleType.Mafia);
                    shuffledListPlayers.Remove(mafiaPlayer);
                    remainingPlayers -= 1;
                }
                //Defines the citizens
                for (int i = 0; i < remainingPlayers; i++)
                {
                    var citizen = utils.GetRandomPlayer(shuffledListPlayers);
                    Roles.Add(citizen, RoleType.Citizen);
                    Players.Remove(citizen);
                    remainingPlayers -= 1;
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

        public void Start(int playersNum, List<string> names)
        {
            //Starts the main game loop, alternating between Day and Night phases.
            InitializeGame(playersNum, names);
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

        public void ExecuteNightPhase()
        {
            //Handles all actions for the Night phase.
            CurrentPhase = GamePhase.Night;
        }

        public void ExecuteDayPhase()
        {
            //Handles voting and discussions for the Day phase.
            CurrentPhase = GamePhase.Day;
        }

        public bool IsThereAWinnerYet()
        {
            //Determines if the game has ended and which side has won.
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

        public void DisplayResults()
        {
            //Shows the final roles and outcome of the game.

            var results = $"Here are the final results: \r\n Total number of rounds:{RoundCount} \r\n Winner:{Winner}";
        }
    }
}
