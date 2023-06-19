using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Interaction
{
    public class TouchHandler : MonoBehaviour
    {
        [SerializeField]
        private RectTransform
            SelfRectTransform; //Better to assign this on the Unity inspector than handling a GetComponent. GetComponent is expensive and, generally, a bad practice.

    
        // I know this can make things worse in the long run when talking about code. Since these events are assigned on the Unity Editor UI instead of directly from the code
        // the reference it has and what it's calling is harder to track. As far as I know only Rider(IDE) can track these.
        // Still I decided to make it this way so this component becomes a substitute for the classic UI Button, with Hold, Hold End and Tap.

        [SerializeField] private UnityEvent OnHold;
        [SerializeField] private UnityEvent OnHoldEnd;
        [SerializeField] private UnityEvent OnTap;
    
        // Start is called before the first frame update
        [SerializeField] private float TimeToHold = 3f;
        private bool Holding = false;
        private float HoldCounter = 0;

        private IEnumerator TapOrHold()
        {
            HoldCounter = 0;
            Holding = false;
        
            while (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(SelfRectTransform, Input.mousePosition, Camera.main))
            {
                yield return null;
                HoldCounter += Time.deltaTime;
                if (HoldCounter >= TimeToHold && !Holding)
                {
                    Holding = true;
                    OnHold.Invoke();
                }
            }
            if(HoldCounter < 3 && RectTransformUtility.RectangleContainsScreenPoint(SelfRectTransform, Input.mousePosition, Camera.main))
            {
                OnTap.Invoke();
            }
            if(Holding) OnHoldEnd.Invoke();
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) &&
                RectTransformUtility.RectangleContainsScreenPoint(SelfRectTransform, Input.mousePosition, Camera.main))
            {
                StopAllCoroutines();//Only one coroutine should run here at given time
                StartCoroutine(TapOrHold());
            }
        }
        // This will be kept here until I test it on mobile
        // if(Input.touchCount > 0)
        // {
        //     Touch touchInfo = Input.GetTouch(0);
        //     if (RectTransformUtility.RectangleContainsScreenPoint (SelfRectTransform ,touchInfo.position))
        //     {
        //         if (touchInfo.phase == TouchPhase.Began) return;
        //         
        //         if (touchInfo.phase == TouchPhase.Stationary)
        //         {
        //             Holding = true;
        //             Debug.Log("is pressing and holding");
        //             float remainingDuration = HoldDuration -= Time.deltaTime;
        //
        //             if (HoldDuration <= 0)
        //             {
        //                 Debug.Log("Show tooltip");
        //                 //Do Stuff when timer is finished
        //             }
        //         }
        //
        //         if (touchInfo.phase == TouchPhase.Ended && !Holding)
        //         {
        //             Debug.Log("Tapped hero");
        //             //Do tap stuff
        //         }
        //     }
        //     
        // }
        // }
    }
}
