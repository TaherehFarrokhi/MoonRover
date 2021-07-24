using System;
using System.ComponentModel;

namespace MoonRover
{
    public sealed class PlutoRover
    {
        public PlutoRover(int x, int y, Direction direction)
        {
            if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0) throw new ArgumentOutOfRangeException(nameof(y));
            if (!Enum.IsDefined(typeof(Direction), direction))
                throw new InvalidEnumArgumentException(nameof(direction), (int) direction, typeof(Direction));
            
            X = x;  
            Y = y;
            Direction = direction;
        }

        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }
    }
}