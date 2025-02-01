using Palermo.Models;
using Palermo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Enums;

namespace Palermo.Domain.Services
{
    public static class VotingService
    {
        public static List<Player> PlayersVoted { get; set; } = [];
        public static Player? StartVotingService(Player voter, Player target, List<Player> players)
        {
            voter.Vote(target);

            var playersVoted = players.Where(x => x.HasPlayerVoted = true).ToList();
            foreach (var player in playersVoted)
            {
                PlayersVoted.Add(player);
            }

          if (players.Count == PlayersVoted.Count)
            {
               var eliminatedPlayer = GetEliminatedPlayer(players);
               foreach (var player in players) 
                {
                    player.ResetVotes();
                }
               return eliminatedPlayer;
            }
          else
            {
                return null;
            }
        }
        public static Player GetEliminatedPlayer(List<Player> players)
        {
            //Determines the player with the most votes.
            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                players.OrderByDescending(x => x.Votes);
            }
            return players[0];
        }
    } 
}
