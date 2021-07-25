using System;
using System.Collections.Generic;

namespace MoonRover
{
    public sealed class PlutoEnvironment
    {
        private readonly List<Location> _obstacles = new();

        public PlutoEnvironment(int height, int width)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            Height = height;
            Width = width;
        }

        public int Width { get; }
        public int Height { get; }
        public IEnumerable<Location> Obstacles => _obstacles;

        public void AddObstacle(int x, int y)
        {
            _obstacles.Add(new Location(x, y));
        }
    }
}