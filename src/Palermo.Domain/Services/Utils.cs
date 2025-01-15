using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;

namespace Palermo.Models
{
    internal class Utils
    {
        public Player GetRandomPlayer(List<Player> players)
        {
            //Selects a random player from the list.
            Random random = new Random();
            var randomInt = random.Next(0, players.Count);
            Player player = players[randomInt];
            return player;
        }

        public void ShuffleList<T>(List<T> list)
        {
            //Shuffles a list (useful for assigning roles).
            Random random = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                var randomInt = random.Next(0, list.Count);
                var index = list[randomInt];
                list.Remove(index);
                list.Add(index);
            }
        }
    }
}
