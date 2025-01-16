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

        public List<T> ShuffleList<T>(List<T> list)
        {
            //Shuffles a list (useful for assigning roles).
            var listCopy = list;
            var shuffledList = new List<T>();   

            Random random = new Random();
            for (int i = 0; i < listCopy.Count; i++)
            {
                var randomInt = random.Next(0, listCopy.Count);
                var item = listCopy[randomInt];
                listCopy.Remove(item);
                shuffledList.Add(item);
            }

            return shuffledList;
        }
    }
}
