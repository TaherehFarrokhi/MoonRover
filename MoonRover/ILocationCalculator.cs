namespace MoonRover
{
    public interface ILocationCalculator
    {
        Location CalculateLocation(char operation, Position current);
    }
}