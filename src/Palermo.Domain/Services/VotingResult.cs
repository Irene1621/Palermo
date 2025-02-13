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
        public static Player EliminatedPlayer { get; set; }
        public static List<Player> PlayersVoted { get; set; } = [];
        public static List<Player> AlivePlayers { get; set; } = [];
    }
}
