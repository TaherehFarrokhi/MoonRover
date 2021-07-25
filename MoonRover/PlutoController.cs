using System;
using System.Linq;

namespace MoonRover
{
    public sealed class PlutoController : IPlutoController
    {
        private readonly PlutoEnvironment _environment;
        private readonly IDirectionCalculator _directionCalculator;
        private readonly ILocationCalculator _locationCalculator;
        private readonly PlutoRover _rover;

        public PlutoController(PlutoEnvironment environment, IDirectionCalculator directionCalculator, ILocationCalculator locationCalculator)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _directionCalculator = directionCalculator ?? throw new ArgumentNullException(nameof(directionCalculator));
            _locationCalculator = locationCalculator ?? throw new ArgumentNullException(nameof(locationCalculator));

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
                    case Operations.Backward:
                        var (success, location) = Move(operation);
                        if (!success)
                            return CommandResult.Error(_rover.Position, $"Rover encountered an obstacle in {location.X}, {location.Y}");
                        break;
                }
            }

            return CommandResult.Success(_rover.Position);
        }

        private (bool Sucess, Location Location) Move(char operation)
        {
            var target = _locationCalculator.CalculateLocation(operation, _rover.Position);

            if (_environment.Obstacles.Any(obstacle => obstacle.X == target.X && obstacle.Y == target.Y))
                return (false, target);

            _rover.Position = _rover.Position with {X = target.X, Y = target.Y};
            return (true, target);
        }

        private void ChangeDirection(char operation)
        {
            var direction = _directionCalculator.CalculateDirection(operation, _rover.Position.Direction);
            _rover.Position = _rover.Position with {Direction = direction};
        }
    }
}