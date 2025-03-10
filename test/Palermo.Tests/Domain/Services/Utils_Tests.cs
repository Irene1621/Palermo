using Palermo.Domain.Services;
using Palermo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Palermo.Services;

namespace Palermo.Tests.Domain.Services
{
    public class Utils_Tests
    {
        [Fact]
        public void GetRandomPlayerTest()
        {
            //Arrange
            var utils = new Utils();
            var player1 = new Citizen(1, "Eirini");
            var player2 = new Citizen(2, "Debbie");
            var players = new List<Player> { player1, player2 };
            //Act
            utils.GetRandomPlayer(players);
            //Assert
            Assert.
        }

        [Fact]
        public void GetPlayerTest()
        {
            //Arrange
            var utils = new Utils();
            var player1 = new Citizen(1, "Eirini");
            var player2 = new Citizen(2, "Debbie");
            var players = new List<Player> { player1, player2 };
            //Act
            var shuffledList = utils.ShuffleList2(players);
            //Assert
            Assert.NotSame(players, shuffledList);
        }

    }
}
