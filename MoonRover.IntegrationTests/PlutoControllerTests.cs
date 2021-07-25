using FluentAssertions;
using Xunit;

namespace MoonRover.IntegrationTests
{
    public class PlutoControllerTests
    {
        [Theory]
        [InlineData("FFRFF", 2, 2, Direction.E)]
        [InlineData("FFFRFFF", 3, 3, Direction.E)]
        [InlineData("FFFrFFf", 3, 3, Direction.E)]
        [InlineData("FBFBFF", 0, 2, Direction.N)]
        [InlineData("FRRRB", 1, 1, Direction.W)]
        public void ExecuteCommand_ShouldReturnTheCorrectLocation_WhenCommandIsValid(string command, int expectedX,
            int expectedY, Direction expectedDirection)
        {
            // Arrange
            var expectedLocation = new Position(expectedX, expectedY, expectedDirection);
            var environment = new PlutoEnvironment(100, 100);
            var sut = new PlutoController(environment, new DirectionCalculator(), new LocationCalculator(environment));
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Position.Should().Be(expectedLocation);
        }
        
        [Theory]
        [InlineData("FFAZDRFF", 2, 2, Direction.E)]
        [InlineData("NNNNNNNNNNNN", 0, 0, Direction.N)]
        [InlineData("", 0, 0, Direction.N)]
        public void ExecuteCommand_ShouldReturnTheCorrectLocation_WhenCommandContainsInvalidOperation(string command, int expectedX,
            int expectedY, Direction expectedDirection)
        {
            // Arrange
            var expectedLocation = new Position(expectedX, expectedY, expectedDirection);
            var environment = new PlutoEnvironment(100, 100);
            var sut = new PlutoController(environment, new DirectionCalculator(), new LocationCalculator(environment));
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Position.Should().Be(expectedLocation);
        }
        
        [Theory]
        [InlineData("BLF", 100, 100, Direction.W)]
        [InlineData("BLFRF", 100, 0, Direction.N)]
        [InlineData("FFFFRB", 100, 4, Direction.E)]
        [InlineData("FFFFLFRR", 100, 4, Direction.E)]
        public void ExecuteCommand_ShouldReturnTheCorrectLocation_WhenLocationIsOnTheEdgeAndOperationMovesRoverOutsideBoundary(string command, int expectedX,
            int expectedY, Direction expectedDirection)
        {
            // Arrange
            var expectedLocation = new Position(expectedX, expectedY, expectedDirection);
            var environment = new PlutoEnvironment(100, 100);
            var sut = new PlutoController(environment, new DirectionCalculator(), new LocationCalculator(environment));
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Position.Should().Be(expectedLocation);
        }
        
        [Theory]
        [InlineData("FFFrFFf", 0, 1, Direction.N)]
        public void ExecuteCommand_ShouldReturnTheLastLocationAndReportTheObstacle_WhenRoverEncounterAnObstacle(string command, int expectedX,
            int expectedY, Direction expectedDirection)
        {
            // Arrange
            var expectedLocation = new Position(expectedX, expectedY, expectedDirection);
            var environment = new PlutoEnvironment(100, 100);
            environment.AddObstacle(0, 2);
            var sut = new PlutoController(environment, new DirectionCalculator(), new LocationCalculator(environment));
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Position.Should().Be(expectedLocation);
            actual.Failed.Should().BeTrue();
            actual.FailedReason.Should().Be("Rover encountered an obstacle in 0, 2");
        }
    }
}