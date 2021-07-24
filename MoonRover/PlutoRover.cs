namespace MoonRover
{
    public sealed class PlutoRover
    {
        public PlutoRover(string name, Position position)
        {
            Name = name;
            Position = position;
        }
        public string Name { get; }
        public Position Position { get; set; }
    }
}