using Controllers;
using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using EventType = Services.EventPipeline.Events.Interfaces.EventType;

namespace Services.EventPipeline.Events
{
    public class TurnEvent : ITurnEvent
    {
        public EventType Type { get; }
        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }

        public Turn NewTurn { get; }

        public TurnEvent(Turn newTurn)
        {
            NewTurn = newTurn;
        }
    }
}
