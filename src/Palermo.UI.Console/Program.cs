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
        public List<string> playersNames { get; set; }

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
                }

            }
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Palermo! Please enter the number of players:");
            var numberOfPlayers = Console.ReadLine();
            int.TryParse(numberOfPlayers, out int result);

            Console.Clear();
            for (int i = 0; i < result; i++)
            {
                Console.WriteLine("Enter name:");
                var playerName = Console.ReadLine();
                playersNames.Add(playerName);
            }

            game.Start(result, playersNames);
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
    }
}
