using System.Collections;
using Services.EventPipeline;
using Services.EventPipeline.Events;
using Services.EventPipeline.Events.Interfaces;
using UnityEngine;

namespace Controllers
{
    public class TurnController : EventHandler
    {
        [SerializeField] private Turn CurrentTurn;
    
        private IEnumerator Start()
        {
            EventPipelineService.Instance.RegisterListener(Visit);

            yield return null; // Wait a frame for the listeners to properly register
        
            EventPipelineService.Instance.Raise(new TurnEvent(Turn.Player));
        }

        public override void Visit(IEvent gameEvent)
        {
            gameEvent.Visit(this);
        }

        public override void Handle(ICombatEvent combatEvent) //Not the best of practices I admit, but since only a single combat event may happen at a given turn, it signals that a turn just ended
        {
            var NewTurn = Turn.Enemy;
        
            switch (CurrentTurn)
            {
                case Turn.Enemy:
                    CurrentTurn = NewTurn = Turn.Player;
                    break;
                case Turn.Player:
                    CurrentTurn = NewTurn = Turn.Enemy;
                    break;
                default:
                    CurrentTurn = NewTurn = Turn.Player;
                    break;
            }

            var NewTurnEvent = new TurnEvent(NewTurn);
            StartCoroutine(PacedNewTurn(NewTurnEvent));
        }

        private IEnumerator PacedNewTurn(ITurnEvent newTurn)
        {
            yield return new WaitForSeconds(1f);
        
            EventPipelineService.Instance.Raise(newTurn);
        }
    }

    public enum Turn //Making it enum, in the future if things get complex, there might be a environment turn or a 3rd party turn. Making it already as an enum is a good practice even though it seems overkill
    {
        Player,
        Enemy
    }
}