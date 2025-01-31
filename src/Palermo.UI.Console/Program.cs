using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Models;
using Palermo.Enums;
using Palermo.Services;

namespace Palermo.Domain
{
    internal class Program
    {
        Game game = new Game();
        public List<string> playersNames { get; set; } = [];

        public static void Main()
        {
            Program program = new Program();
            program.Menu();
        }

        public void Menu()
        {
            Console.Clear();

            List<string> Menu = new List<string>
            {
            "1. Start Game",
            "2. Info",
            "3. Exit"
            };


            foreach (var menu in Menu)
            {
                var stringMenu = menu.ToString();
                Console.WriteLine(stringMenu);

            }

            var userAnswer = Console.ReadLine();

            if (int.TryParse(userAnswer, out int result))
            {
                switch (result)
                {
                    case 1:
                        //Start game
                        StartGame();
                        break;

                    case 2:
                        //Info
                        ProvideInfo();
                        break;
                    case 3:
                        //Exit
                        break;
                    default:
                        break;
                }

            }
        }

        public void StartGame()
        {
            Vote vote1 = new Vote();
            Console.Clear();
            Console.WriteLine("Welcome to Palermo! Please enter the number of players:");
            var numberOfPlayers = Console.ReadLine();
            int.TryParse(numberOfPlayers, out int result);
            if (result >= 5)
            {
                Console.Clear();
                for (int i = 1; i <= result; i++)
                {
                    Console.WriteLine("Enter name:");
                    var playerName = Console.ReadLine();
                    playersNames.Add(playerName);
                }

                game.Start(result, playersNames);

                for (int i = 0; i < playersNames.Count; i++)
                {
                    Console.Clear();
                    var name = playersNames[i];
                    Console.WriteLine($"{name}\r\nPress any key to reveal role");
                    Console.ReadLine();
                    var player = FindPlayer(name);
                    Console.WriteLine(player.Role);
                    Console.WriteLine("Next player");
                    Console.ReadLine();
                }
                Console.Clear();

                for (game.RoundCount = 0; game.RoundCount < 10; game.RoundCount++)
                {
                    while (game.CurrentPhase == GamePhase.Day)
                    {
                        game.ExecuteDayPhase();
                        for (int i = 0; i < result; i++)
                        {
                            var name = playersNames[i];
                            Console.WriteLine($"{name}, who do you want to vote?");
                            var vote = Console.ReadLine();
                            var voter = FindPlayer(name);
                            var player = FindPlayer(vote);
                            if (player != null && voter != null)
                            {
                                vote1.CastVote(voter.Id, player.Id);
                                Console.WriteLine("Next player");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Player not found");
                            }

                        }
                        //var eliminatedPlayerId = vote1.GetEliminatedPlayerId();
                        //int.TryParse(eliminatedPlayerId, out int playerId);
                        //FindPlayerById(playerId);

                        var isThereAWinnerYet = game.IsThereAWinnerYet();
                        if (isThereAWinnerYet == true)
                        {
                            game.DisplayResults();
                        }
                        else
                        {
                            game.ExecuteNightPhase();
                            Console.WriteLine("Night falls in Palermo. All the players close their eyes. Press any key to begin night phase");
                            Console.ReadLine();
                            Console.WriteLine($"Mafia players open your eyes. Who do you want to vote?");
                            var playerKilled = Console.ReadLine();
                            var player = FindPlayer(playerKilled);
                            player.IsAlive = false;
                            Console.WriteLine("Mafia players close your eyes.");
                            Console.ReadLine();
                            Console.WriteLine("Day comes in Palermo again. All the players open their eyes.\r\nPress any key to reveal the player the mafia killed");
                            Console.ReadLine();
                            Console.WriteLine($"{playerKilled} you have been murdered.");
                            game.ExecuteDayPhase();


                        }
                    }
                }
               
                
            }
            else
            {
                Console.WriteLine("The number of players should be at least 5");
            }
            
            Console.WriteLine("\r\nPress any key to return to menu");
            Console.ReadLine();
            Menu();
        }

        public void ProvideInfo()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Palermo!\r\n The game should consist of at least 5 players. Each player gets assigned a role. Players are divided into teams based on some characters, the good team (citizens and detective) and the bad team (mafia). The job of the good team is to stay alive until the end of the game and eliminate the mafia players. The job of the bad team is to kill all the good players and stay alive until the end of the game. In the game there are 2 mafia players, 1 detective and the rest of the players are citizens\r\n \r\nDay phase:\r\nEach game starts with the Day Phase. Immediately the players start a conversation to figure out who the killers (mafia) are. Then, when all the players have expressed their suspicions and made a decision, the voting begins. In turn, each player votes for the player they think is the killer or who they want to eliminate from the game. Then, the player with the most votes is eliminated from the game and the night phase begins.\r\n \r\nNight phase:\r\nAfter the voting, comes the night phase. During the night, the killers decide who they want to kill and inform the player who died most recently. Then, when Day Phase comes again the eliminated player informs the group about the mafia's decision, without revealing their identities. \r\n \r\nMafia player: In the game there are 2 mafia players. His job is to eliminate the detective and all the other players. Both mafia players know their partner's identity and during the game they work together to achieve their goal and win.\r\n \r\nDetective: His job is to find the killers and eliminate them from the game. He knows the identity of one of the mafia players and during the game he helps the rest of the citizens figure out who the killers are, without directly revealing his identity.\r\n \r\nCitizen: He doesn't have any special abilities, but his job is to stay alive until the end of the game and figuring out who the killers are. He has no clue about anyone's identity.");
            Console.WriteLine("\r\nPress any key to return to menu");
            Console.ReadLine();
            Menu();
        }

        /// <summary>
        /// Searches for a player by name and returns him if found.
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public Player? FindPlayer(string playerName)
        {
            for (int i = 0; i < game.Players.Count; i++)
            {
                var player = game.Players[i];

                if (player.Name.Contains(playerName))
                {
                    return player;
                }
            }
            return null;
        }

        public Player? FindPlayerById(int playerId)
        {
            for (int i = 0; i < game.Players.Count; i++)
            {
                var player = game.Players[i];

                if (player.Id == playerId)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
