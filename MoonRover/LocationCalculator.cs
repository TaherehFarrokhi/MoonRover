using System;

namespace MoonRover
{
    public sealed class LocationCalculator : ILocationCalculator
    {
        private readonly PlutoEnvironment _environment;
        private const int ForwardStep = 1;
        private const int BackwardStep = -1;

        public LocationCalculator(PlutoEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        
        public Location CalculateLocation(char operation, Position current)
        {
            return operation switch
            {
                Operations.Backward => CalculateNewLocation(BackwardStep, current),
                Operations.Forward => CalculateNewLocation(ForwardStep, current),
                _ => current
            };
        }
        
        private Location CalculateNewLocation(int step, Position current)
        {
            var (x, y) = current.Direction switch
            {
                Direction.N => (current.X, current.Y + step),
                Direction.E => (current.X + step, current.Y),
                Direction.S => (current.X, current.Y - step),
                Direction.W => (current.X - step, current.Y),
                _ => (current.X, current.Y)
            };

            return new Location(JustifyCoordinate(x, _environment.Width), JustifyCoordinate(y, _environment.Height));
        }
        
        private static int JustifyCoordinate(int coordinate, int max) =>
            coordinate < 0 ? max : coordinate > max ? 0 : coordinate;
    }
}