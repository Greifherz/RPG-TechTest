using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using EventType = Services.EventPipeline.Events.Interfaces.EventType;

namespace Services.EventPipeline.Events
{
    public class CombatEvent : ICombatEvent
    {
        public EventType Type { get; }

        public int Damage { get; }
        public int Target { get; }
        public CombatEventType CombatEventType { get; }

        public void Visit(IEventHandler handler)
        {
            handler.Handle(this);
        }

        public CombatEvent(int target, int damage) //When there's skills, another constructor will have to be made to get the right event type
        {
            CombatEventType = CombatEventType.Attack;
            Damage = damage;
            Target = target;
        }

        public override string ToString() // for logging and testing
        {
            return Type + " Target " + Target + " damage " + Damage;
        }
    }
}
