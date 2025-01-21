using Palermo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Palermo.Models
{
    internal class Vote
    {
        public Dictionary<int, int> Votes { get; set; }
        public Dictionary<int, int> Players { get; set; }

        /// <summary>
        /// Records a vote from one player to another.
        /// </summary>
        /// <param name="voterId"></param>
        /// <param name="targetId"></param>
        public void CastVote(int voterId, int targetId)
        {
            //Records a vote from one player to another.
            Votes.Add(voterId, targetId);
            if (Players.ContainsKey(targetId))
            {
                Players[targetId] += 1;
            }
            else
            {
            Players.Add(targetId, 1);
            }
        }

        /// <summary>
        /// Determines the player with the most votes.
        /// </summary>
        public void GetEliminatedPlayerId()
        {
            //Determines the player with the most votes.
            for (int i = 0; i < Players.Count; i++)
            {
                var player = Players[i];
                var playerVotes = player;
            }
        }
    }
}
