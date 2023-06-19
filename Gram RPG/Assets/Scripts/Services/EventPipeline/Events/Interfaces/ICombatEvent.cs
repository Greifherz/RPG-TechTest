using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.EventPipeline.Events.Interfaces
{
    public interface ICombatEvent : IEvent
    {
    
        int Damage { get; } //Basically the attacker's attack power
        int Target { get; } //It goes as the following:0 - 1st hero, 1 - 2nd hero, 2 - 3rd hero, 3 - enemy
        //Doing this as an int will make it smoother to randomize the target
        CombatEventType CombatEventType { get; }
    }

    public enum CombatEventType // So far only attack type makes sense, but in the future if there are skills or other types of moves this will be useful
    {
        Attack, 
    }
}