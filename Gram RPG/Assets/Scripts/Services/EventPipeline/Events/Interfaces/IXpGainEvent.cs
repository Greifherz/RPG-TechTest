namespace Services.EventPipeline.Events.Interfaces
{
    public interface IXpGainEvent : IEvent
    {
        int XpGained { get; }
    }
}
