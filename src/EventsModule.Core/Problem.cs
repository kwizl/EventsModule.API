namespace EventsModule.Core
{
    public record Problem
    {
        public int ErrorCode { get; init; }

        public string Description { get; init; } = null!;

        public Exception? Exception { get; init; }
    }
}