using UnityEngine;

namespace UI.View
{
    public class MobileSafeAreaElement : MonoBehaviour
    {
        //This whole component is only necessary since there are some android models (and ios) that have a smaller view area on the top of the screen.
        //Unity does not automatically detects it and lowers it's elements, so it must be done in a way similar to this.
        //Safe areas are important for mobile game dev, especially if the game has standard banners (Dominoes had one on the lowest edge of the screen).
    
        // Start is called before the first frame update
        void Start()
        {
#if !UNITY_ANDROID && !UNITY_IOS //I rather have these precompiler ifs than check the environment in runtime
        gameObject.SetActive(false);
#endif
        }

    }
}
