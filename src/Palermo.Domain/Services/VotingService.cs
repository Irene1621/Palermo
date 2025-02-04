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
        public static void StartVotingService(Player voter, Player target, List<Player> players)
        {
            voter.Vote(target);

            var playersVoted = players.Where(x => x.HasPlayerVoted == true).ToList();
            foreach (var player in playersVoted)
            {
                PlayersVoted.Add(player);
            }

        }
        
    } 
}
