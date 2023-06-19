using System.Collections.Generic;

namespace Unit
{
    public class UnitPool
    {
        public int BattleCount = 0; //Has to stay public to persist into json
    
        public List<UnitData> UnitsInPool = new List<UnitData>();
        public int[] SelectedUnits;

        public void AddBattleCount()
        {
            if (++BattleCount % 5 == 0)
            {
                AddUnitToPool();
            }
        }
    
        private void AddUnitToPool() //Should be called every 5 battles
        {
            if(UnitsInPool.Count >= 10) return;
        
            var UnitToAdd = UnitRandomizer.Instance.GenerateRandomUnitData();
            UnitsInPool.Add(UnitToAdd);
        }

        public void RemoveUnitFromPool(UnitData data) //Should be called whenever a hero dies
        {
            for (int i = 0; i < UnitsInPool.Count; i++)
            {
                if (UnitsInPool[i].Equals(data))
                {
                    UnitsInPool.RemoveAt(i);
                    break;
                }
            }
        }

        public UnitPool() //Whenever you create a new pool, the 3 "default" heroes are generated
        {
            for(int i = 0 ; i < 3 ; i++)
                UnitsInPool.Add(UnitRandomizer.Instance.GenerateRandomUnitData());
        }
    }
}
