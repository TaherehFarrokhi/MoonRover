namespace MoonRover
{
    public sealed class PlutoRover
    {
        public PlutoRover(string name, Location location)
        {
            Name = name;
            Location = location;
        }
        public string Name { get; }
        public Location Location { get; set; }
    }
}