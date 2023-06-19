using Unit;
using UnityEngine;

namespace Services.Persistence
{
    public static class PersistenceExtensions //Specialized methods shouldn't extend the interface, but rather be extended through extension methods.
    {
        public static void SaveUnitPool(this IPersistenceService self, UnitPool pool)
        {
            var SerializedPool = JsonUtility.ToJson(pool);
            self.Save(SerializedPool,"UnitPool");
        }

        public static UnitPool LoadUnitPool(this IPersistenceService self)
        {
            var LoadedSerializedPool = self.Load("UnitPool");
            return LoadedSerializedPool.Length == 0 ? new UnitPool() : JsonUtility.FromJson<UnitPool>(LoadedSerializedPool);
        }

        public static bool HasUnitPoolData(this IPersistenceService self)
        {
            return self.HasData("UnitPool");
        }

        public static void SaveCombatData(this IPersistenceService self, CombatPersistentData combatData)
        {
            var SerializedCombatData = JsonUtility.ToJson(combatData);
            self.Save(SerializedCombatData,"CombatData");
        }

        public static CombatPersistentData LoadCombatData(this IPersistenceService self)
        {
            var LoadedSerializedCombatData = self.Load("CombatData");
            return LoadedSerializedCombatData.Length == 0 ? new CombatPersistentData() : JsonUtility.FromJson<CombatPersistentData>(LoadedSerializedCombatData);
        }

        public static bool HasStoredCombatData(this IPersistenceService self)
        {
            return self.HasData("CombatData");
        }

        public static void ClearCombatData(this IPersistenceService self)
        {
            self.ClearPersistence("CombatData");
        }
    }
}
