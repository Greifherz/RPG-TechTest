using System.Collections;
using System.Collections.Generic;
using Controllers;
using Services.EventPipeline;
using Services.EventPipeline.Events;
using Services.EventPipeline.Events.Interfaces;
using UI.View;
using Unit;
using UnityEngine;
using EventHandler = Services.EventPipeline.EventHandler;

namespace UI
{
    public class CombatUnit : EventHandler
    {
        [SerializeField] private int UnitIndex;
    
        [SerializeField] private FloatingText FloatingText;
        [SerializeField] private UnitView UnitView;
        [SerializeField] private HpBar HpBar;

        [SerializeField] private bool AutoAttack = false;
    
        private UnitData Data;
        private bool CanAttack = false;
        private List<int> AttackableIndexes = new List<int>();
    
        private void Start()
        {
            EventPipelineService.Instance.RegisterListener(Visit);
        
            if(UnitIndex < 3) AttackableIndexes.Add(3);
            else
            {
                AttackableIndexes = new List<int>{0,1,2};
            }
        }

        public void SetUnitData(UnitData dataToSet)
        {
            Data = dataToSet;
            UnitView.SetUnitData(dataToSet);
            HpBar.UpdateFill((float)Data.Health / Data.MaxHealth);
        }

        public override void Visit(IEvent gameEvent)
        {
            gameEvent.Visit(this);
        }

        public void Attack()
        {
            if (AttackableIndexes.Count == 0 || !CanAttack || Data.Health <= 0) return;
        
            EventPipelineService.Instance.Raise(new AttackStartedEvent());
        
            var combatEvent = new CombatEvent(AttackableIndexes[UnityEngine.Random.Range(0,AttackableIndexes.Count)], Data.AttackPower);
            UnitView.AnimateAttack(() => EventPipelineService.Instance.Raise(combatEvent));
        
        }

        public override void Handle(ICombatEvent combatEvent)
        {
            if (combatEvent.Target >= 3 || UnitIndex >= 3) //Another hero attacked this turn or is enemy
            {
                CanAttack = false;
            }
            if(combatEvent.Target != UnitIndex) return; //Is not attacking this hero

            Data.Health -= combatEvent.Damage;
        
            //Animate take damage
            UnitView.AnimateTakeDamage(() =>
            {
                UnitView.UpdateUnitDataView();
                HpBar.UpdateFill((float)Data.Health / Data.MaxHealth);
        
                //Won't check null since Handle should not be called outside the battle scene
                FloatingText.SetValue(combatEvent.Damage);
        
                if (Data.Health <= 0)
                {
                    UnitView.AnimateDeath(()=> EventPipelineService.Instance.Raise(new DeathEvent(UnitIndex)));
                }
            });
        }

        public override void Handle(ITurnEvent turnEvent)
        {
            CanAttack = UnitIndex < 3 ? turnEvent.NewTurn == Turn.Player : turnEvent.NewTurn == Turn.Enemy;
            if (CanAttack && AutoAttack && Data.Health > 0)
                StartCoroutine(WaitedAttack());
        }

        public override void Handle(IDeathEvent deathEvent)
        {
            AttackableIndexes.Remove(deathEvent.DeathIndex);
        }

        public override void Handle(IAttackStartedEvent attackEvent)//This is an event just to prevent multiple attacks at the same turn
        {
            CanAttack = false;
        }

        public override void Handle(IXpGainEvent xpEvent)
        {
            if(Data.Health <= 0) return;
        
            Data.Experience += xpEvent.XpGained;
            while (Data.Experience >= (5 * Data.Level))
            {
                ++Data.Level;
                Data.MaxHealth = Mathf.CeilToInt(Data.MaxHealth * 1.1f);
                Data.AttackPower = Mathf.CeilToInt(Data.AttackPower * 1.1f);
                FloatingText.ShowMessage("Level Up! Atk + 10% Hp + 10%");
            }
        }

        private IEnumerator WaitedAttack() //Used this random wait so in the future I can add an autoplay, in which random heroes attack automatically AND in case of multiple enemies
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1,2));
        
            if(CanAttack)
                Attack();
        }
    }
}
