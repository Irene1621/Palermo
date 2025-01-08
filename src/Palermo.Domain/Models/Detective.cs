using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Models;

namespace Palermo.Domain
{
    internal class Detective : Player
    {
        public override void PerformNightAction(Game game)
        {
            //Detective investigates a player's role
        }
    }
}
