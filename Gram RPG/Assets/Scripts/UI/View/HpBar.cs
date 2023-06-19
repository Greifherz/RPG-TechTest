using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image Foreground;

        public void UpdateFill(float proportion)
        {
            var UsedProportion = proportion < 0 ? 0 : proportion;
            var NewColor = Color.green;
            Foreground.transform.DOScaleX(UsedProportion, 1f);
            if (UsedProportion < 0.3f)
                NewColor = Color.red;
            else if (UsedProportion < 0.6f)
                NewColor = Color.yellow;
            Foreground.DOColor(NewColor, 1f);
        }

        // private void Update()
        // {
        //     if(Input.GetKeyUp(KeyCode.A))
        //         UpdateFill(0.25f);
        //     if(Input.GetKeyUp(KeyCode.S))
        //         UpdateFill(0.5f);
        //     if(Input.GetKeyUp(KeyCode.D))
        //         UpdateFill(0.75f);
        //     if(Input.GetKeyUp(KeyCode.F))
        //         UpdateFill(1);
        // }
    }
}
