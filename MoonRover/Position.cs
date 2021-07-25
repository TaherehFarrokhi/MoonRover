namespace MoonRover
{
    public record Location(int X, int Y);

    public sealed record Position(int X, int Y, Direction Direction) : Location(X, Y)
    {
        public override string ToString()
        {
            return $"({X}, {Y}) faced to {Direction}";
        }
    }
}