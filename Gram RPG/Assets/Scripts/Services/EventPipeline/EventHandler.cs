using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using UnityEngine;

namespace Services.EventPipeline
{
    public abstract class EventHandler : MonoBehaviour, IEventHandler
    {
        public abstract void Visit(IEvent gameEvent);

        public virtual void Handle(ICombatEvent combatEvent)
        {
            //The virtual implementation is the one that does nothing at all
        }

        public virtual void Handle(ITurnEvent turnEvent)
        {
            //The virtual implementation is the one that does nothing at all
        }

        public virtual void Handle(IDeathEvent deathEvent)
        {
            //The virtual implementation is the one that does nothing at all
        }

        public virtual void Handle(IXpGainEvent xpEvent)
        {
            //The virtual implementation is the one that does nothing at all
        }

        public virtual void Handle(IAttackStartedEvent attackEvent)
        {
            //the virtual implementation is the one that does nothing at all
        }

        public virtual void Handle(ICombatEndEvent endEvent)
        {
            //the virtual implementation is the one that does nothing at all
        }
    }
}
