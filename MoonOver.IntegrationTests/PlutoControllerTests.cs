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
        public void ExecuteCommand_ShouldReturnTheRightLocation_WhenCommandIsValid(string command, int expectedX,
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