using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Models;

namespace Palermo.Domain
{
    internal interface IRole
    {
        public void PerformNightAction(Game game);
    }
}
