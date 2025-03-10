using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain;
using Palermo.Models;
using Xunit;

namespace Palermo.Tests.Domain.Models
{
    
    public class Player_Tests
    {
        [Fact]
        public void AddVoteTest()
        {
            //Arrange
            var player = new Citizen(1, "Eirini");
            //Act
            player.AddVote();
            //Assert
            Assert.Equal(1, player.Votes);
        }

        [Fact]
        public void EliminateTest()
        {
            //Arrange
            var player = new Citizen(1, "Eirini");
            //Act
            player.Eliminate();
            //Assert
            Assert.False(player.IsAlive);  
        }

        [Fact]
        public void ShouldThrowExceptionPlayerNotAliveTest()
        {
            //Arrange
            var player = new Citizen(1, "Eirini");
            var target = new Citizen(1, "Debbie");
            target.IsAlive = false;
            //Act
            player.Vote(target);
            //Assert
        }

        [Fact]
        public void ShouldThrowExceptionPlayerCantVoteThemselvesTest()
        {
            //Arrange
            var player = new Citizen(1, "Eirini");
            //Act
            player.Vote(player);
            //Assert
        }

        [Fact]
        public void ResetVotesTest()
        {
            //Arrange
            var player = new Citizen(1, "Eirini");
            //Act
            player.AddVote();
            player.ResetVotes();
            //Assert
            Assert.Equal(0, player.Votes);
        }
    }
}
