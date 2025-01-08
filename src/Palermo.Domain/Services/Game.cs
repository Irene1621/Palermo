using System;
using System.Collections.Generic;
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

        public void InitializeGame(int numberOfPlayers, List<string> playerNames)
        {
            //Sets up the game by assigning roles randomly.
            Utils utils = new Utils();
            CurrentPhase = GamePhase.Day;
            var player = utils.GetRandomPlayer;
            
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
                    var result = CheckVictoryConditions();
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
        }

        public void ExecuteDayPhase()
        {
            //Handles voting and discussions for the Day phase.
        }

        public bool CheckVictoryConditions()
        {
            //Determines if the game has ended and which side has won.
            if(Roles.Where(x => x.Value == RoleType.Detective).Count() == 0)
            {
                Winner = "Villagers";
                return true;
            }
            else if(Roles.Where(x => x.Value == RoleType.Mafia).Count() == 0)
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
