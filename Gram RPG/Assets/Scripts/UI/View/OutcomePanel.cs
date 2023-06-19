using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class OutcomePanel : MonoBehaviour
    {
        [SerializeField] private Text OutcomeText;
        [SerializeField] private Image OutcomeBackground;
        [SerializeField] private Image ButtonBackground;
        [SerializeField] private Text ButtonText;
    
        public void Show(bool win)
        {
            OutcomeText.text = win ? "You won!" : "You lost!";
            gameObject.SetActive(true);

            OutcomeBackground.DOFade(1, 2);
            OutcomeText.DOFade(1, 2);
            ButtonBackground.DOFade(1, 2);
            ButtonText.DOFade(1, 2);
        }
    }
}
