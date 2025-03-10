using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Palermo.Domain.Services;
using Palermo.Models;
using Palermo.Services;
using Xunit;

namespace Palermo.Tests.Domain.Services
{
    public class Vote_Tests
    {
        [Fact]
        public void StartVotingTest()
        {
            //Arrange
            var voter = new Citizen(1, "Eirini");
            var target = new Citizen(2, "Debbie");
            var players = new List<Player> { voter, target };
            //Act
            VotingService.StartVotingService(voter, target, players);
            //Assert
            Assert.Equal(1, target.Votes);
            Assert.True(voter.HasPlayerVoted);
            Assert.Contains(voter, VotingResult.PlayersVoted);
        }

        [Fact]
        public void ShouldGetVotingResultsTest()
        {
            //Arrange
            var voter = new Citizen(1, "Eirini");
            var target = new Citizen(2, "Debbie");
            var players = new List<Player> { voter, target };
            //Act
            VotingService.StartVotingService(voter, target, players);
            VotingService.GetVotingResults(players);
            //Assert
            Assert.Equal(0, target.Votes);
            Assert.Equal(0, voter.Votes);
            Assert.Equal(target, VotingResult.EliminatedPlayer);
        }

        [Fact]
        public void ShouldGetEliminatedPlayerTest()
        {
            //Arrange
            var voter = new Citizen(1, "Eirini");
            var target = new Citizen(2, "Debbie");
            var players = new List<Player> { voter, target };
            //Act
            VotingService.StartVotingService(voter, target, players);
            VotingService.GetEliminatedPlayer(players);
            //Assert
            Assert.Equal(target, VotingResult.EliminatedPlayer);
        }
    }
}
