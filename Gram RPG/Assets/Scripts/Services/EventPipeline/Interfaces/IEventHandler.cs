using Services.EventPipeline.Events.Interfaces;

namespace Services.EventPipeline.Interfaces
{
    public interface IEventHandler
    {
        void Visit(IEvent gameEvent);

        void Handle(ICombatEvent combatEvent);

        void Handle(ITurnEvent turnEvent);

        void Handle(IDeathEvent deathEvent);

        void Handle(IXpGainEvent xpEvent);

        void Handle(IAttackStartedEvent attackEvent);

        void Handle(ICombatEndEvent endEvent);
    }
}