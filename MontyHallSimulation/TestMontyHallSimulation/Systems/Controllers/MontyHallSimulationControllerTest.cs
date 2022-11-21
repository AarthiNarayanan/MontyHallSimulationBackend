using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

namespace TestMontyHallSimulation.Systems.Controllers
{
    public class MontyHallSimulationControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<MontyHallLogic> _serviceMock;
        private readonly MontyHallSimulationController _sut;

        public MontyHallSimulationControllerTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<MontyHallLogic>>();
            _sut = new MontyHallSimulationController(_serviceMock.Object);
        }


        [Fact]
        public void TestResults_Should_ReturnResult_WhenDataFound()
        {
            //Arrange
            var montyHallLogicMock = _fixture.Create<Result>();
            _serviceMock.Setup(x => x.PlayGames(10, true)).Returns(montyHallLogicMock);

            //Act
            var result = _sut.Results(new Request { inputChoice=true,inputData=10});

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().As<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(montyHallLogicMock.GetType());
            _serviceMock.Verify(x => x.PlayGames(10, true), Times.Once());
        }

        [Fact]
        public void TestResults_Should_ReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            Result results = null;
            _serviceMock.Setup(x => x.PlayGames(10, true)).Returns(results);

            //Act
            var result = _sut.Results(new Request { inputChoice = true, inputData = 10 });

            result.Should().NotBeNull();
            result.Result.Should().As<NotFoundResult>();
            _serviceMock.Verify(x => x.PlayGames(10, true),Times.Once());
        }

        [Fact]
        public void TestResults_Should_ReturnResult_WhenValidInput()
        {
            //Arrange
            var montyHallLogicMock = _fixture.Create<Result>();
            Request request = _fixture.Create<Request>();
            _serviceMock.Setup(x => x.PlayGames(request.inputData, request.inputChoice)).Returns(montyHallLogicMock);
            //Act
            var result = _sut.Results(request);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().As<OkObjectResult>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(montyHallLogicMock.GetType());
            _serviceMock.Verify(x => x.PlayGames(request.inputData, request.inputChoice), Times.Once());
        }

        [Fact]
        public void TestResults_Should_ReturnResult_WhenInValidInput()
        {
            //Arrange
            var montyHallLogicMock = _fixture.Create<Result>();
            Request request = _fixture.Create<Request>();
            _serviceMock.Setup(x => x.PlayGames(request.inputData, request.inputChoice)).Returns(montyHallLogicMock);
            //Act
            var result = _sut.Results(new Request {inputData = -1, inputChoice = true });

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().As<BadRequestResult>();  
            _serviceMock.Verify(x => x.PlayGames(request.inputData, request.inputChoice), Times.Never());

        }

    }
}
