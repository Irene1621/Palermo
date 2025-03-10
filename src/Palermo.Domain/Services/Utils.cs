using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Enums;
using Palermo.Models;

namespace Palermo.Services
{
    public class Utils
    {
        /// <summary>
        /// Selects a random player from the list.
        /// </summary>
        /// <param name="players"></param>
        /// <returns>the random player</returns>
        public Player GetRandomPlayer(List<Player> players)
        {
            //Selects a random player from the list.
            Random random = new Random();
            var playersCount = players.Count;
            playersCount -= 1;
            var randomInt = random.Next(0, playersCount);
            Player player = players[randomInt];
            return player;
        }

        /// <summary>
        /// Shuffles a list (useful for assigning roles).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>the shuffled list</returns>
        //public List<T> ShuffleList<T>(List<T> list)
        //{
        //    //Shuffles a list (useful for assigning roles).
        //    var listCopy = list;
        //    var shuffledList = new List<T>();   

        //    Random random = new Random();
        //    for (int i = 0; i < listCopy.Count; i++)
        //    {
        //        var randomInt = random.Next(0, listCopy.Count);
        //        var item = listCopy[randomInt];
        //        listCopy.Remove(item);
        //        shuffledList.Add(item);
        //    }

        //    return shuffledList;
        //}


        public List<T> ShuffleList2<T>(List<T> list)
        {
            var listCopy = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                listCopy.Add(item);
            }

            for (int i = 0; i < listCopy.Count; i++)
            {
                var lastIndex = listCopy.Count - 1;
                Random random = new Random();
                var randomItem = random.Next(0, listCopy.Count);
                var randomItemValue = listCopy[randomItem];
                listCopy[randomItem] = listCopy[lastIndex];
                listCopy[lastIndex] = randomItemValue;
            }
            return listCopy;
        }
    }
}
