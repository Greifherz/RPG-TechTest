using Services.EventPipeline;
using Services.EventPipeline.Events.Interfaces;
using Services.Persistence;
using UI;
using Unit;
using UnityEngine;

namespace Controllers
{
    public class BattleBootstrapper : EventHandler
    {
        public override void Visit(IEvent gameEvent)
        {
            gameEvent.Visit(this);
        }
    
        [SerializeField] private CombatUnit[] Heroes;
    
        [SerializeField] private CombatUnit Enemy;

        private UnitPool UnitPool;

        private CombatPersistentData CombatData;

        private int DeadHeroesCount = 0;
        private IPersistenceService PersistenceService;

        private void Start()
        {
            PersistenceService = new PlayerPrefsPersistenceService();
        
            UnitPool = PersistenceService.LoadUnitPool();

            if (PersistenceService.HasStoredCombatData())
            {
                CombatData = PersistenceService.LoadCombatData();
            }
            else
            {
                CombatData = new CombatPersistentData();
                var EnemyData = UnitRandomizer.Instance.GenerateRandomUnitData();
                EnemyData.UnitColor = Color.red;
                CombatData.CombatUnits = new[] { UnitPool.UnitsInPool[UnitPool.SelectedUnits[0]],UnitPool.UnitsInPool[UnitPool.SelectedUnits[1]],UnitPool.UnitsInPool[UnitPool.SelectedUnits[2]],EnemyData };
            }
        
            EventPipelineService.Instance.RegisterListener(Visit);

            InitializeUnits(CombatData);
        }

        private void InitializeUnits(CombatPersistentData combatData)
        {
            for (int i = 0; i < combatData.CombatUnits.Length; i++)
            {
                if (i < 3)
                {
                    Heroes[i].SetUnitData(combatData.CombatUnits[i]);
                }
                else
                {
                    Enemy.SetUnitData(combatData.CombatUnits[i]);
                }
            }
        }

        public override void Handle(ITurnEvent turnEvent)
        {
            if (turnEvent.NewTurn == Turn.Player)
            {
                PersistenceService.SaveCombatData(CombatData);
            }
        }

        public override void Handle(ICombatEndEvent endEvent)
        {
            UnitPool.AddBattleCount();
            PersistenceService.ClearCombatData();
        
            for (int i = 0; i < UnitPool.SelectedUnits.Length; i++) // Restore health to heroes that battled
            {
                var CurrentUnitData = UnitPool.UnitsInPool[UnitPool.SelectedUnits[i]];
                CurrentUnitData.Health = CurrentUnitData.MaxHealth;
            }
            PersistenceService.SaveUnitPool(UnitPool);
        }
    }
}
