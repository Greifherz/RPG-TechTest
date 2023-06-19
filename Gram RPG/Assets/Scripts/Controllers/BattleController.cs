using System.Collections;
using Services.EventPipeline;
using Services.EventPipeline.Events;
using Services.EventPipeline.Events.Interfaces;
using Services.SceneTransition;
using UI.View;
using UnityEngine;
using EventHandler = Services.EventPipeline.EventHandler;

namespace Controllers
{
    public class BattleController : EventHandler
    {
        [SerializeField] private OutcomePanel OutcomePanel;
    
        private int DeadHeroesCount = 0;

        private void Start()
        {
            EventPipelineService.Instance.RegisterListener(Visit);
        }

        public override void Visit(IEvent gameEvent)
        {
            gameEvent.Visit(this);   
        }

        public override void Handle(IDeathEvent deathEvent)
        {
            if(deathEvent.DeathIndex == 3) StartCoroutine(EndBattle(true));
            else
            {
                if(++DeadHeroesCount == 3)
                    StartCoroutine(EndBattle(false));
            }
        }

        private IEnumerator EndBattle(bool win)
        {
            if (win)
            {
                EventPipelineService.Instance.Raise(new XpGainEvent(1));
            }
            EventPipelineService.Instance.Raise(new CombatEndEvent());
            
            yield return new WaitForSeconds(1.5f);
            
            OutcomePanel.Show(win);
        }

        public void GoToHeroSelection()
        {
            SceneTransitionService.Instance.TransitionTo(SceneIndexes.HeroSelection);
        }
    }
}
