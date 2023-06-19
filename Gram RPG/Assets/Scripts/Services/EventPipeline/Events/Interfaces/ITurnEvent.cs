using Controllers;

namespace Services.EventPipeline.Events.Interfaces
{
    public interface ITurnEvent : IEvent
    {
        Turn NewTurn { get; }
    }
}
