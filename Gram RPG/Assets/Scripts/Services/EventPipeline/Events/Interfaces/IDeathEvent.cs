namespace Services.EventPipeline.Events.Interfaces
{
    public interface IDeathEvent : IEvent
    {
        int DeathIndex { get; }
    }
}
