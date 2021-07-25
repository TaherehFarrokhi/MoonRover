using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MoonRover
{
    public class DirectionCalculator : IDirectionCalculator
    {
        private readonly Dictionary<Direction, (Direction Left, Direction Right)> _directionMap =
            new()
            {
                {Direction.N, (Direction.W, Direction.E)},
                {Direction.S, (Direction.E, Direction.W)},
                {Direction.W, (Direction.S, Direction.N)},
                {Direction.E, (Direction.N, Direction.S)}
            };
        public Direction CalculateDirection(char operation, Direction current)
        {
            if (operation <= 0) throw new ArgumentOutOfRangeException(nameof(operation));
            if (!Enum.IsDefined(typeof(Direction), current))
                throw new InvalidEnumArgumentException(nameof(current), (int) current, typeof(Direction));
            
            return operation switch
            {
                Operations.TurnLeft => _directionMap[current].Left,
                Operations.TurnRight => _directionMap[current].Right,
                _ => current
            };
        }
    }
}