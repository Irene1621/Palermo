using Palermo.Enums;
using Palermo.Models;
using Palermo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Palermo.Tests.Domain.Services
{
    public class Game_Tests
    {
        [Fact]
        public void ShouldExecuteNightPhase()
        {
            //Arrange
            var game = new Game();
            //Act
            game.ExecuteNightPhase();
            //Assert
            Assert.Equal(GamePhase.Night, game.CurrentPhase);
        }

        [Fact]
        public void ShouldExecuteDayPhase()
        {
            //Arrange
            var game = new Game();
            //Act
            game.ExecuteDayPhase();
            //Assert
            Assert.Equal(GamePhase.Day, game.CurrentPhase);
        }

        [Fact]
        public void IsThereAWinnerYetTest1()
        {
            //Arrange
            var game = new Game();
            var player1 = new Mafia(1, "Eirini");
            var player2 = new Detective(2, "Debbie");
            player1.Eliminate();
            //Act
            game.IsThereAWinnerYet();
            //Assert
            Assert.Equal("Villagers", game.Winner);
        }

        [Fact]
        public void IsThereAWinnerYetTest2()
        {
            //Arrange
            var game = new Game();
            var player1 = new Mafia(1, "Eirini");
            var player2 = new Detective(2, "Debbie");
            player2.Eliminate();
            //Act
            game.IsThereAWinnerYet();
            //Assert
            Assert.Equal("Mafia", game.Winner);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAssigningRolesTest()
        {
            //Arrange
            var game = new Game();
            List<string> Names = new List<string> { "Eirini", "Debbie", "Spyros", "Fili", "Eve" };
            //Act
            game.AssignRoles(4, Names);
            //Assert
            Assert.Throws("Number of players should be at least five or number of players isn't equal to the players names list count") , AssignRolesTest)
        }

        [Fact]
        public void AssignRolesTest()
        {
            //Arrange
            var game = new Game();
            List<string> Names = new List<string> { "Eirini", "Debbie", "Spyros", "Fili", "Eve" };
            var Detectives = game.Players.Where(x => x.Role == RoleType.Detective).ToList();
            var Mafia = game.Players.Where(x => x.Role == RoleType.Mafia).ToList();
            var Citizens = game.Players.Where(x => x.Role == RoleType.Citizen).ToList();
            //Act
            game.AssignRoles(5, Names);
            //Assert
            Assert.Single(Detectives);
            Assert.Equal(2, Mafia.Count);
            Assert.Equal(2, Citizens.Count);
        }
    }
}
