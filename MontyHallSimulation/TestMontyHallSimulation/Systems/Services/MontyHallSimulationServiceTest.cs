using AutoFixture;
using FluentAssertions;
using MontyHallSimulation.BusinessLogic;
using MontyHallSimulation.Controllers;
using MontyHallSimulation.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestMontyHallSimulation.Systems.Services
{
    public class MontyHallSimulationServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<MontyHallLogic> _serviceMock;
        private readonly MontyHallLogic _sut;

        public MontyHallSimulationServiceTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<MontyHallLogic>>();
            _sut = new MontyHallLogic();
        }


        [Fact]
        public void TestResults_Should_ReturnResult_WhenValidInput()
        {
            //Arrange
            var montyHallWinMock = _fixture.Create<bool>();
            var montyHallNoOfGamesMock = _fixture.Create<int>();
            var montyHallChoiceMock = _fixture.Create<bool>();
            var pickedDoorMock = _fixture.Create<int>();
            var carDoorMock = _fixture.Create<int>();
            var goatDoorToRemoveMock = _fixture.Create<int>();
            _serviceMock.Setup(x => x.IsAWin(pickedDoorMock, montyHallChoiceMock, carDoorMock, goatDoorToRemoveMock)).Returns(montyHallWinMock);

            //Act
            var result = _sut.PlayGames(10, montyHallChoiceMock);

            //Assert
            result.Should().NotBeNull();
            result.Loss.Should().Match(x=> x >= 0);
            result.Wins.Should().Match(x=> x >= 0);
            result.NoOfChances.Should().Match(x=> x == result.Wins+result.Loss);
           
        }
    }
}
