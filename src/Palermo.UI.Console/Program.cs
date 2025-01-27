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
        public void Initialize()
        {
           

        }
        public static void Main()
        {
            Program program = new Program();
            program.Initialize();
            program.Menu();
        }

        public void Menu()
        {
            Console.Clear();

            List<string> Menu = new List<string>
            {
            "1. Start Game",
            "2. Settings",
            "3. Info",
            "4. Exit"
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
                        //Settings

                        break;
                    case 3:
                        //Info

                        break;
                    case 4:
                        //Exit
                        break;
                }

            }
        }

        public void StartGame()
        {
            Console.Clear();
            game.Start(6, playersNames);
            Menu();
        }

    }
}
