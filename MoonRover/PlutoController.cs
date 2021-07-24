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

        private Dictionary<Direction, (Direction Left, Direction Right)> _directionMap =
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
            _rover = new PlutoRover(0, 0, Direction.N);
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
            switch(_rover.Direction) {
 
                case Direction.N:
                    _rover.Y += step; 
                    break;
                case Direction.E:
                    _rover.X += step;
                    break;
                case Direction.S:
                    _rover.Y -= step;
                    break;
                case Direction.W:
                    _rover.X -= step;
                    break;
            }
        }

        private void ChangeDirection(char operation)
        {
            _rover.Direction = operation switch
            {
                Operations.TurnLeft => _directionMap[_rover.Direction].Left,
                Operations.TurnRight => _directionMap[_rover.Direction].Right,
                _ => _rover.Direction
            };
        }
    }
}