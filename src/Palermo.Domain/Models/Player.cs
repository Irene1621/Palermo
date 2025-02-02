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

        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsAlive { get; set; }
        public RoleType Role { get; private set; }
        public int Votes { get; private set; }
        public bool HasPlayerVoted { get; private set; }

        protected Player(int id, string name, RoleType role)
        {
            Id = id;
            Name = name;
            Role = role;
            IsAlive = true;
            Votes = 0;
            HasPlayerVoted = false;
        }
        public abstract void PerformNightAction(Game game);


        public void AddVote()
        {
            Votes++;
        }

        public void Eliminate()
        {
            IsAlive = false;
        }

        public void Vote(Player target)
        {
            if (!target.IsAlive) 
            { 
                throw new Exception("The player you want to vote isn't alive");
            }

            if (target.Name == Name) 
            { 
                throw new Exception("Player can't vote for themselves");
            }
            if (HasPlayerVoted == true)
            {
                throw new Exception("Player has already voted");
            }
            target.AddVote();
            HasPlayerVoted = true;
        }

        public void ResetVotes()
        {
            Votes = 0;
            HasPlayerVoted = false;
        }


    }
}
