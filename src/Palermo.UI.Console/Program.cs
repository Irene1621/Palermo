using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Models;
using Palermo.Enums;
using Palermo.Services;
using Palermo.Domain.Services;

namespace Palermo.Domain
{
    internal class Program
    {
        Game game = new Game();
        public int NumberOfPlayers { get; set; }
        public List<string> PlayersNames { get; set; } = [];
        public List<Player> Players { get; set; } = [];

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
            
            AssignRoles();

            foreach (var player in game.Players)
            {
                Players.Add(player);
            }

            for (game.RoundCount = 0; game.RoundCount < 10; game.RoundCount++)
            {
                    StartVoting();
                
                    var isThereAWinnerYet = game.IsThereAWinnerYet();
                if (isThereAWinnerYet == true)
                {
                    var results = game.DisplayResults();
                    Console.WriteLine(results);
                    break;
                }
                else
                {
                    StartNightPhase();

                    var isThereAWinnerAfterNightPhase = game.IsThereAWinnerYet();

                    if (isThereAWinnerAfterNightPhase == true)
                    {
                        var results = game.DisplayResults();
                        Console.WriteLine(results);
                        break;
                    }
                    game.ExecuteDayPhase();

                }
            }
            ResetGame();
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

        public void AssignRoles()
        {

            Console.Clear();
            Console.WriteLine("Welcome to Palermo! Please enter the number of players:");
            var playersNumber = Console.ReadLine();
            int.TryParse(playersNumber, out int result);
            NumberOfPlayers = result;
            if (NumberOfPlayers >= 5)
            {
                Console.Clear();
                for (int i = 1; i <= result; i++)
                {
                    Console.WriteLine("Enter name:");
                    var playerName = Console.ReadLine();
                    PlayersNames.Add(playerName);
                }

                game.Start(NumberOfPlayers, PlayersNames);

                for (int i = 0; i < PlayersNames.Count; i++)
                {
                    Console.Clear();
                    var name = PlayersNames[i];
                    Console.WriteLine($"{name}\r\nPress any key to reveal role");
                    Console.ReadLine();
                    var player = game.FindPlayer(name);
                    Console.WriteLine(player.Role);
                    Console.WriteLine("Next player");
                    Console.ReadLine();
                }
                Console.Clear();
            }
            else
            {
                Console.WriteLine("The number of players should be at least 5");
            }
        }

        public void StartVoting()
        {
            game.ExecuteDayPhase();
            for (int i = 0; i < PlayersNames.Count; i++) 
            {
                var name = PlayersNames[i];
                Console.WriteLine($"{name}, who do you want to vote?");
                
                bool FoundPlayer = false;

                while (FoundPlayer == false)
                {
                    var vote = Console.ReadLine();
                    var voter = game.FindPlayer(name);
                    var player = game.FindPlayer(vote);
                    if (player != null && voter != null)
                    {
                        FoundPlayer = true; 
                        VotingService.StartVotingService(voter, player, Players);

                        Console.WriteLine("Next player");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Player not found. Please enter a valid name");
                        FoundPlayer = false;
                    }
                }
            }
            var eliminatedPlayer = VotingResult.GetVotingResults(Players);
            eliminatedPlayer.Eliminate();
            //if (result != null)
            //{
            Console.WriteLine($"{eliminatedPlayer.Name} you have been eliminated");
            PlayersNames.Remove(eliminatedPlayer.Name);
            //}
            //else
            //{

            //}
        }

        public void StartNightPhase()
        {
            game.ExecuteNightPhase();
            Console.WriteLine("Night falls in Palermo. All the players close their eyes. Press any key to begin night phase");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Mafia players open your eyes. Who do you want to vote?");
            bool FoundPlayer = false;
            while (FoundPlayer == false)
            {
                var playerKilled = Console.ReadLine();
                var player = game.FindPlayer(playerKilled);
                if (player != null && playerKilled != null)
                {
                    FoundPlayer = true;
                    player.Eliminate();
                    PlayersNames.Remove(player.Name);
                    Console.WriteLine("Mafia players close your eyes.");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Day comes in Palermo again. All the players open their eyes.\r\nPress any key to reveal the player the mafia killed");
                    Console.ReadLine();
                    Console.WriteLine($"{playerKilled} you have been murdered.");
                    Console.WriteLine("Press any key to start voting");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Player not found. Please enter a valid name");
                    FoundPlayer = false;
                }
            }
        }

        public void ResetGame()
        {
            Players.Clear();
            PlayersNames.Clear();
            
        }
    }
}
