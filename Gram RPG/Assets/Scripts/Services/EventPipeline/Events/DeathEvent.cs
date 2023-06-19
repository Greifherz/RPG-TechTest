using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using EventType = Services.EventPipeline.Events.Interfaces.EventType;

namespace Services.EventPipeline.Events
{
    public class DeathEvent : IDeathEvent
    {
        public EventType Type { get; }

        public int DeathIndex { get; }
    
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }

        public DeathEvent(int deathIndex)
        {
            Type = EventType.Combat;
            DeathIndex = deathIndex;
        }
    }
}
