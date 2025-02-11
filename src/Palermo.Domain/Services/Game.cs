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
using Palermo.Domain.Services;
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
        public string Winner { get; set; }
        public List<int> PlayersId { get; set; } = [];


        /// <summary>
        /// Starts the main game loop, alternating between Day and Night phases.
        /// </summary>
        /// <param name="playersNum"></param>
        /// <param name="names"></param>
        public void Start(int playersNumber, List<string> names)
        {
            GeneratePlayersId();
            AssignRoles(playersNumber, names);
            CurrentPhase = GamePhase.Day;

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
            if (Players.Where(x => x.Role == RoleType.Mafia && x.IsAlive == true).Count() == 0)
            {
                Winner = "Villagers";
                return true;
            }
            else if (Players.Where(x => x.Role == RoleType.Detective && x.IsAlive == true).Count() == 0)
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
        public string DisplayResults()
        {

            var results = $"Here are the final results: \r\n Total number of rounds:{RoundCount} \r\n Winner:{Winner}";
            return results;
        }

        /// <summary>
        /// Assigns roles randomly
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="playerNames"></param>
        /// <returns></returns>
        public void AssignRoles(int numberOfPlayers, List<string> playerNames)
        {
            if (numberOfPlayers != playerNames.Count || numberOfPlayers < 5)
            {
                throw new ArgumentException("Number of players should be at least five or number of players isn't equal to the players names list count");
            }

            Utils utils = new Utils();
            var error = string.Empty;
            var remainingPlayers = numberOfPlayers;
            var shuffledListNames = utils.ShuffleList2(playerNames);


            for (int i = 0; i <= shuffledListNames.Count; i++)
            {
                var name = shuffledListNames[i];
                var detectiveId = PickId();
                var detective = new Detective(detectiveId, name);
                Players.Add(detective);
                remainingPlayers -= 1;
                shuffledListNames.Remove(name);

                for (int x = 0; x < 2; x++)
                {
                    var name2 = shuffledListNames[i];
                    var mafiaId = PickId();
                    var mafia = new Mafia(mafiaId, name2);
                    Players.Add(mafia);
                    remainingPlayers -= 1;
                    shuffledListNames.Remove(name2);
                }

                for (int y = 0; y < remainingPlayers + 1; y++)
                {
                    var name3 = shuffledListNames[i];
                    var citizenId = PickId();
                    var citizen = new Citizen(citizenId, name3);
                    Players.Add(citizen);
                    remainingPlayers -= 1;
                    shuffledListNames.Remove(name3);
                }
            }
        }

        /// <summary>
        /// Searches for a player by name and returns him if found.
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public Player? FindPlayer(string playerName)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                var player = Players[i];

                if (player.Name.Contains(playerName))
                {
                    return player;
                }
            }
            return null;
        }

        public void GeneratePlayersId()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                PlayersId.Add(i);
            }
        }

        public int PickId()
        {
            Random random = new Random();
            var randomId = random.Next(0, PlayersId.Count);
            PlayersId.Remove(randomId);
            return randomId;
        }
    }
}
