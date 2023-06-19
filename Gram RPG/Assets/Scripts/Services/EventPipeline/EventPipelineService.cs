using System;
using System.Collections;
using System.Collections.Generic;
using Services.EventPipeline.Events.Interfaces;
using Services.EventPipeline.Interfaces;
using UnityEngine;


//Quick rundown on how this works:
//    Listeners are registers on the EventPipelineService
//    Since so far there's only need for a single EventPipelineService (There could be others, specialized, to reduce overhead) all will be registered on this which is a singleton
//    A script may raise an event, if so it get pooled to be sent on the next end of frame to prevent stuttering in case something heavy is calculated
//    Once sent all event handlers receive the event as it's basicmost interface, IEvent. In turn when they receive they send to the event their handler to the event object through the "Visit" method
//    When visit is called, it send the handler it's real, uppermost, interface, with all it's data.

//    This is a combination between 2 patterns, Visitor (the visit call to get the uppermost interface) and Command pattern (The events are commands).

//    When implementing, each listener should implement (or not, since the handles are virtual on an abstract class) how they should handle each event.

// Again, since it's yet simple a single "thread" of pipeline should work, but for more complex projects it would be the best practice
// to have a couple of these running instead so there aren't many visit and handle calls that won't actually do anything.

// I usually work with 3 of these. One for internal systems, one only for animations and one for networking events.
// They may communicate through adapters, that are listeners that convert from the pipelines, such as a networking event that needs to become an animation, etc.

namespace Services.EventPipeline
{
    public class EventPipelineService : MonoBehaviour, IEventPipelineService
    {
        public static EventPipelineService Instance; //Really don't like singletons, but this will make things easier to access the pipeline
    
        private event Action<IEvent> EventPipeline = (gameEvent) => { };

        private Queue<IEvent> EventPool = new Queue<IEvent>();
    
        private void Awake() //I've changed the script execution order so it runs before anything else
        {                    //This is something to keep an eye on, thanks to the singleton so it doesn't become an anti-pattern
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            StartCoroutine(Pooling());
        }

        public void RegisterListener(Action<IEvent> listenAction)
        {
            EventPipeline += listenAction;
        }

        public void UnregisterListener(Action<IEvent> listenAction)
        {
            EventPipeline -= listenAction;
        }

        public void Raise(IEvent gameEvent)
        {
            EventPool.Enqueue(gameEvent);
        }

        private IEnumerator Pooling() //Since it's a coroutine I won't have issues of making things happen outside unity's main thread
        {
            while(gameObject != null)//Bad condition check, should improve later. Checking the gameobject for null is a bad practice since it's not performatic.
            {
                for (int i = 0; i < EventPool.Count; i++)
                {
                    EventPipeline(EventPool.Dequeue());
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}