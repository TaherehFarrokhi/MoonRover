using FluentAssertions;
using Xunit;

namespace MoonRover.UnitTests
{
    public class LocationCalculatorTests
    {
        [Theory]
        [InlineData(0, 0, Direction.N, Operations.Forward, 0, 1)]
        [InlineData(0, 0, Direction.E, Operations.Forward, 1, 0)]
        [InlineData(0, 0, Direction.W, Operations.Forward, 3, 0)]
        [InlineData(0, 0, Direction.S, Operations.Forward, 0, 3)]
        [InlineData(0, 0, Direction.N, Operations.Backward, 0, 3)]
        [InlineData(0, 0, Direction.E, Operations.Backward, 3, 0)]
        [InlineData(0, 0, Direction.W, Operations.Backward, 1, 0)]
        [InlineData(0, 0, Direction.S, Operations.Backward, 0, 1)]
        [InlineData(1, 1, Direction.N, Operations.Forward, 1, 2)]
        [InlineData(1, 1, Direction.E, Operations.Forward, 2, 1)]
        [InlineData(1, 1, Direction.W, Operations.Forward, 0, 1)]
        [InlineData(1, 1, Direction.S, Operations.Forward, 1, 0)]
        [InlineData(1, 1, Direction.N, Operations.Backward, 1, 0)]
        [InlineData(1, 1, Direction.E, Operations.Backward, 0, 1)]
        [InlineData(1, 1, Direction.W, Operations.Backward, 2, 1)]
        [InlineData(1, 1, Direction.S, Operations.Backward, 1, 2)]
        [InlineData(3, 3, Direction.N, Operations.Forward, 3, 0)]
        [InlineData(3, 3, Direction.E, Operations.Forward, 0, 3)]
        [InlineData(3, 3, Direction.W, Operations.Forward, 2, 3)]
        [InlineData(3, 3, Direction.S, Operations.Forward, 3, 2)]
        [InlineData(3, 3, Direction.N, Operations.Backward, 3, 2)]
        [InlineData(3, 3, Direction.E, Operations.Backward, 2, 3)]
        [InlineData(3, 3, Direction.W, Operations.Backward, 0, 3)]
        [InlineData(3, 3, Direction.S, Operations.Backward, 3, 0)]
        [InlineData(0, 3, Direction.N, Operations.Forward, 0, 0)]
        [InlineData(0, 3, Direction.E, Operations.Forward, 1, 3)]
        [InlineData(0, 3, Direction.W, Operations.Forward, 3, 3)]
        [InlineData(0, 3, Direction.S, Operations.Forward, 0, 2)]
        [InlineData(0, 3, Direction.N, Operations.Backward, 0, 2)]
        [InlineData(0, 3, Direction.E, Operations.Backward, 3, 3)]
        [InlineData(0, 3, Direction.W, Operations.Backward, 1, 3)]
        [InlineData(0, 3, Direction.S, Operations.Backward, 0, 0)]
        public void CalculateDirection_ShouldReturnCorrectLocation_WhenOperationIsValid(int currentX, int currentY,
            Direction currentDirection, char operation, int expectedX, int expectedY)
        {
            // Arrange
            var plutoEnvironment = new PlutoEnvironment(3, 3);
            var sut = new LocationCalculator(plutoEnvironment);

            // Act
            var actual = sut.CalculateLocation(operation, new Position(currentX, currentY, currentDirection));
            
            // Assert

            actual.X.Should().Be(expectedX);
            actual.Y.Should().Be(expectedY);
        }
    }
}