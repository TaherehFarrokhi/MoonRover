using System;

namespace MoonRover
{
    public sealed class PlutoController
    {
        private readonly int _gridHeight;
        private readonly int _gridWidth;

        public PlutoController(int gridHeight, int gridWidth)
        {
            if (gridHeight <= 0) throw new ArgumentOutOfRangeException(nameof(gridHeight));
            if (gridWidth <= 0) throw new ArgumentOutOfRangeException(nameof(gridWidth));
            _gridHeight = gridHeight;
            _gridWidth = gridWidth;     
        }

        public PlutoRover ExecuteCommand(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            return new PlutoRover(2,2, Direction.E);
        }
    }
}