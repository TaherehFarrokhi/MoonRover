namespace MoonRover
{
    public sealed class CommandResult
    {
        public Position Position { get; init; }
        public bool Failed  { get; init; }
        public string FailedReason  { get; init; }

        public static CommandResult Success(Position position)
        {
            return new() {Position = position};
        }         
        public static CommandResult Error(Position position, string reason)
        {
            return new() {Position = position, Failed = true, FailedReason = reason};
        } 
    }
}