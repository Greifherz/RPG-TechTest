using System;
using Unit;

namespace Services.Persistence
{
    [Serializable]
    public class CombatPersistentData
    {
        public UnitData[] CombatUnits;// the First 3 are heroes, even if they're dead. UnitData is serializable so it can be used to persist all combat data the game has so far.
    }
}
