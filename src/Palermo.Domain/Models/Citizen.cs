using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Models;

namespace Palermo.Domain
{
    internal class Citizen : Player
    {
        public override void PerformNightAction(Game game)
        {
            //Does nothing (no special night actions)
        }
    }
}
