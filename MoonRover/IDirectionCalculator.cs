namespace MoonRover
{
    public interface IDirectionCalculator
    {
        Direction CalculateDirection(char operation, Direction current);
    }
}