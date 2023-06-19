using System.Collections;
using System.Collections.Generic;
using Services.EventPipeline.Interfaces;
using UnityEngine;

namespace Services.EventPipeline.Events.Interfaces
{
    //I have to be careful with this name of class since "event" is a c# keyword.
// But there will be no such thing as "Event" pure class, only interface
    public interface IEvent
    {
        EventType Type { get; }
    
        void Visit(IEventHandler handler);
    
    }

    public enum EventType
    {
        Combat,
        Animation,
    }
}