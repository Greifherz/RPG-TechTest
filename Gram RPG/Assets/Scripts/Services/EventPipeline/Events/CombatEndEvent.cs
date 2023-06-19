using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using EventType = Services.EventPipeline.Events.Interfaces.EventType;

namespace Services.EventPipeline.Events
{
    public class CombatEndEvent : ICombatEndEvent
    {
        public EventType Type { get; }
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }
    
        public CombatEndEvent()
        {
            
        }
    }
}
