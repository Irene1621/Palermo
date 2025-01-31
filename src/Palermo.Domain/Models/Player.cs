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
    public abstract class Player
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAlive { get; set; }
        public RoleType Role { get; set; }

        public abstract void PerformNightAction(Game game);

        public void ReceiveInfo(string info)
        {
            //Allows the player to receive information
        }
    }
}
