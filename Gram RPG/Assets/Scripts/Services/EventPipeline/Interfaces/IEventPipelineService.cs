using System;
using Services.EventPipeline.Events.Interfaces;

namespace Services.EventPipeline.Interfaces
{
    public interface IEventPipelineService
    {
        void Raise(IEvent gameEvent);
        void RegisterListener(Action<IEvent> listenAction);
        void UnregisterListener(Action<IEvent> listenAction);
    }
}
