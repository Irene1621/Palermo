using Palermo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palermo.Domain.Services
{
    public static class VotingResult
    {
        
        public static Player? GetVotingResults(List<Player> players)
        {
            
            var alivePlayers = players.Where(x => x.IsAlive == true).ToList();

            //if (alivePlayers.Count == VotingService.PlayersVoted.Count)
            //{
                var eliminatedPlayer = GetEliminatedPlayer(players);
                foreach (var player in players)
                {
                    player.ResetVotes();
                }
                return eliminatedPlayer;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public static Player GetEliminatedPlayer(List<Player> players)
        {
            //Determines the player with the most votes.
            Player eliminatedPlayer = null;
            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                eliminatedPlayer = players.OrderByDescending(x => x.Votes).First();
            }
            return eliminatedPlayer;
        }
    }
}
