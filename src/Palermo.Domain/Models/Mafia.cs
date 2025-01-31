using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Services;
using Palermo.Enums;

namespace Palermo.Models;

    internal class Mafia : Player
    {
    public Mafia(string name)
    {
        Role = RoleType.Mafia;
        Name = name;
        IsAlive = true;
    }
    public override void PerformNightAction(Game game)
        {
            //Mafia chooses a target to eliminate
        }
    }

