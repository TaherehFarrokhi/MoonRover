using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonRover
{
    public sealed class PlutoController
    {
        private const int ForwardStep = 1;
        private const int BackwardStep = -1;

        private readonly Dictionary<Direction, (Direction Left, Direction Right)> _directionMap =
            new()
            {
                {Direction.N, (Direction.W, Direction.E)},
                {Direction.S, (Direction.E, Direction.W)},
                {Direction.W, (Direction.S, Direction.N)},
                {Direction.E, (Direction.N, Direction.S)}
            };

        private readonly int _gridHeight;
        private readonly int _gridWidth;
        private readonly PlutoRover _rover;
        private readonly List<Location> _obstacles = new ();
        public PlutoController(int gridHeight, int gridWidth)
        {
            if (gridHeight <= 0) throw new ArgumentOutOfRangeException(nameof(gridHeight));
            if (gridWidth <= 0) throw new ArgumentOutOfRangeException(nameof(gridWidth));
            _gridHeight = gridHeight;
            _gridWidth = gridWidth;
            _rover = new PlutoRover("Rover", new Position(0, 0, Direction.N));
        }

        public CommandResult ExecuteCommand(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var operations = command.ToUpper().ToCharArray();
            foreach (var operation in operations)
            {
                switch (operation)
                {
                    case Operations.TurnLeft:
                    case Operations.TurnRight:
                        ChangeDirection(operation);
                        break;
                    case Operations.Forward:
                        var (successForward, targetForward) = Move(ForwardStep);
                        if (!successForward)
                            return CommandResult.Error(_rover.Position, $"Rover encountered an obstacle in {targetForward.X}, {targetForward.Y}");
                        break;
                    case Operations.Backward:
                        var (successBackward, targetBackward) = Move(BackwardStep);
                        if (!successBackward)
                            return CommandResult.Error(_rover.Position, $"Rover encountered an obstacle in {targetBackward.X}, {targetBackward.Y}");
                        break;
                }
            }
            return CommandResult.Success(_rover.Position);
        }

        private (bool Sucess, Location Location) Move(int step)
        {
            var (x, y) = _rover.Position.Direction switch
            {
                Direction.N => (_rover.Position.X, _rover.Position.Y + step),
                Direction.E => (_rover.Position.X + step, _rover.Position.Y),
                Direction.S => (_rover.Position.X, _rover.Position.Y - step),
                Direction.W => (_rover.Position.X - step, _rover.Position.Y),
                _ => (_rover.Position.X, _rover.Position.Y)
            };
            
            var target = new Location(JustifyCoordinate(x, _gridWidth), JustifyCoordinate(y, _gridHeight));
            if(_obstacles.Any(obstacle => obstacle.X == target.X && obstacle.Y == target.Y))
                return (false, target);
            
            _rover.Position = _rover.Position with {X = target.X, Y = target.Y};
            return (true, target);
        }

        private int JustifyCoordinate(int coordinate, int max)
        {
            return coordinate < 0 ? max : coordinate > max ? 0 : coordinate;
        }

        private void ChangeDirection(char operation)
        {
            var direction = operation switch
            {
                Operations.TurnLeft => _directionMap[_rover.Position.Direction].Left,
                Operations.TurnRight => _directionMap[_rover.Position.Direction].Right,
                _ => _rover.Position.Direction
            };
            _rover.Position = _rover.Position with {Direction = direction};
        }

        public void AddObstacle(int x, int y)
        {
            _obstacles.Add(new Location(x, y));
        }
    }
}