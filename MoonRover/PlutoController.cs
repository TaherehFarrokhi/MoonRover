using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonRover
{
    public sealed class PlutoController
    {
        private const int ForwardStep = 1;
        private const int BackwardStep = -1;
        private readonly int _gridHeight;
        private readonly int _gridWidth;
        private readonly PlutoRover _rover;

        private readonly Dictionary<Direction, (Direction Left, Direction Right)> _directionMap =
            new()
            {
                {Direction.N, (Direction.W, Direction.E)},
                {Direction.S, (Direction.E, Direction.W)},
                {Direction.W, (Direction.S, Direction.N)},
                {Direction.E, (Direction.N, Direction.S)}
            };

        public PlutoController(int gridHeight, int gridWidth)
        {
            if (gridHeight <= 0) throw new ArgumentOutOfRangeException(nameof(gridHeight));
            if (gridWidth <= 0) throw new ArgumentOutOfRangeException(nameof(gridWidth));
            _gridHeight = gridHeight;
            _gridWidth = gridWidth;
            _rover = new PlutoRover("Rover", new Location(0,0, Direction.N));
        }

        public PlutoRover ExecuteCommand(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
           
            var operations = command.ToCharArray().ToList();
            operations.ForEach(operation => {
                switch (operation) {
                    case Operations.TurnLeft:
                    case Operations.TurnRight:
                        ChangeDirection(operation);
                        break;
                    case Operations.Forward:
                        Move(ForwardStep);
                        break;
                    case Operations.Backward:
                        Move(BackwardStep);
                        break;
                }
            });
            return _rover;
        }

        private void Move(int step)
        {
            var (x, y) = _rover.Location.Direction switch
            {
                Direction.N => (_rover.Location.X, _rover.Location.Y + step),
                Direction.E => (_rover.Location.X + step, _rover.Location.Y),
                Direction.S => (_rover.Location.X, _rover.Location.Y - step),
                Direction.W => (_rover.Location.X - step, _rover.Location.Y),
                _ => (_rover.Location.X, _rover.Location.Y)
            };

            _rover.Location = _rover.Location with { X = x, Y = y};
        }

        private void ChangeDirection(char operation)
        {
            var direction = operation switch
            {
                Operations.TurnLeft => _directionMap[_rover.Location.Direction].Left,
                Operations.TurnRight => _directionMap[_rover.Location.Direction].Right,
                _ => _rover.Location.Direction
            };
            _rover.Location = _rover.Location with { Direction = direction };
        }
    }
}