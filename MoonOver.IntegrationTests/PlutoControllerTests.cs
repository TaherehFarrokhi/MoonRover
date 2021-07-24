using FluentAssertions;
using MoonRover;
using Xunit;

namespace MoonOver.IntegrationTests
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
            var expectedLocation = new Location(expectedX, expectedY, expectedDirection);
            var sut = new PlutoController(100, 100);
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Location.Should().Be(expectedLocation);
        }
        
        [Theory]
        [InlineData("FFAZDRFF", 2, 2, Direction.E)]
        [InlineData("NNNNNNNNNNNN", 0, 0, Direction.N)]
        [InlineData("", 0, 0, Direction.N)]
        public void ExecuteCommand_ShouldReturnTheCorrectLocation_WhenCommandContainsInvalidOperation(string command, int expectedX,
            int expectedY, Direction expectedDirection)
        {
            // Arrange
            var expectedLocation = new Location(expectedX, expectedY, expectedDirection);
            var sut = new PlutoController(100, 100);
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Location.Should().Be(expectedLocation);
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
            var expectedLocation = new Location(expectedX, expectedY, expectedDirection);
            var sut = new PlutoController(100, 100);
            
            // Act
            var actual = sut.ExecuteCommand(command);

            // Assert
            actual.Should().NotBeNull();
            actual.Location.Should().Be(expectedLocation);
        }
    }
}