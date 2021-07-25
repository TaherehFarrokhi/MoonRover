using FluentAssertions;
using Xunit;

namespace MoonRover.UnitTests
{
    public class DirectionCalculatorTests
    {
        [Theory]
        [InlineData(Direction.N, Operations.TurnLeft, Direction.W)]
        [InlineData(Direction.N, Operations.TurnRight, Direction.E)]        
        [InlineData(Direction.N, 'l', Direction.W)]
        [InlineData(Direction.N, 'r', Direction.E)]
        [InlineData(Direction.S, Operations.TurnLeft, Direction.E)]
        [InlineData(Direction.S, Operations.TurnRight, Direction.W)]
        [InlineData(Direction.S, 'l', Direction.E)]
        [InlineData(Direction.S, 'r', Direction.W)]
        [InlineData(Direction.W, Operations.TurnLeft, Direction.S)]
        [InlineData(Direction.W, Operations.TurnRight, Direction.N)]
        [InlineData(Direction.W, 'l', Direction.S)]
        [InlineData(Direction.W, 'r', Direction.N)]
        [InlineData(Direction.E, Operations.TurnLeft, Direction.N)]
        [InlineData(Direction.E, Operations.TurnRight, Direction.S)]
        [InlineData(Direction.E, 'l', Direction.N)]
        [InlineData(Direction.E, 'r', Direction.S)]
        public void CalculateDirection_ShouldReturnCorrectDirection_WhenOperationIsValidToChangeDirection(Direction currentDirection, char operation,
            Direction expectedDirection)
        {
            // Arrange 
            var sut = new DirectionCalculator();
            
            // Act
            var actual = sut.CalculateDirection(operation, currentDirection);
            
            // Assert

            actual.Should().Be(expectedDirection);
        }
        
        [Theory]
        [InlineData(Direction.N, Operations.Backward, Direction.N)]
        [InlineData(Direction.N, Operations.Forward, Direction.N)]
        [InlineData(Direction.N, 'O', Direction.N)]
        [InlineData(Direction.S, Operations.Backward, Direction.S)]
        [InlineData(Direction.S, Operations.Forward, Direction.S)]
        [InlineData(Direction.S, 'p', Direction.S)]
        [InlineData(Direction.W, Operations.Backward, Direction.W)]
        [InlineData(Direction.W, Operations.Forward, Direction.W)]
        [InlineData(Direction.W, 'm', Direction.W)]
        [InlineData(Direction.E, Operations.Backward, Direction.E)]
        [InlineData(Direction.E, Operations.Forward, Direction.E)]
        [InlineData(Direction.E, 'b', Direction.E)]
        public void CalculateDirection_ShouldReturnSameDirectionAsCurrentDirection_WhenOperationIsNotValid(Direction currentDirection, char operation,
            Direction expectedDirection)
        {
            // Arrange 
            var sut = new DirectionCalculator();
            
            // Act
            var actual = sut.CalculateDirection(operation, currentDirection);
            
            // Assert

            actual.Should().Be(expectedDirection);
        }
    }
}