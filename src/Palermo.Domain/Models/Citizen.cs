using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Services;
using Palermo.Enums;

namespace Palermo.Models
{
    internal class Citizen : Player
    {
        public Citizen(string name)
        {
            Role = RoleType.Citizen;
            Name = name;
        }
        public override void PerformNightAction(Game game)
        {
            //Does nothing (no special night actions)
        }
    }
}
