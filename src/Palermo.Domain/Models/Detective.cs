using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Services;
using Palermo.Enums;

namespace Palermo.Models
{
    internal class Detective : Player
    {
        public Detective(int id, string name) : base (id, name, RoleType.Detective)
        {

        }
        public override void PerformNightAction(Game game)
        {
            //Detective investigates a player's role
        }
    }
}
