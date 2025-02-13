using Palermo.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Palermo.Enums;
using Palermo.Models;

namespace Palermo.Services
{
    public class Vote
    {
        public Dictionary<int, int> Votes { get; set; } = [];
        public Dictionary<int, int> Players { get; set; } = [];

        /// <summary>
        /// Records a vote from one player to another.
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="playerId"></param>
        public void CastVote(int voterId, int playerId)
        {
            //Records a vote from one player to another.
            Votes.Add(voterId, playerId);
            if (Players.ContainsKey(playerId))
            {
                Players[playerId] += 1;
            }
            else
            {
            Players.Add(playerId, 1);
            }
        }

        /// <summary>
        /// Determines the player with the most votes.
        /// </summary>
       
    }
}

// [key, value] PlayerId = 4  Players[2] = 5
// [1, 2]
// [2, 5]
// [4, 5]