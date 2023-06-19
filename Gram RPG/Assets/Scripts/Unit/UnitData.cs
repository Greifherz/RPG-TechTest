using System;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class UnitData
    {
        public string Name;
        public int MaxHealth;
        public int Health;
        public int AttackPower;
        public int Experience;
        public int Level;
        public Color UnitColor;

        public UnitData() //Initializes with default values
        {
            Name = "Unit";
            Health = MaxHealth = 10;
            AttackPower = 2;
            Experience = 0;
            Level = 1;
            UnitColor = Color.white;
        }
    }
}
