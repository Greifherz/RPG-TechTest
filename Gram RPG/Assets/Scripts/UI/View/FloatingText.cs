using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private Text FloatingTextText;
        // Start is called before the first frame update
        void OnEnable() //Actually looked up for references on how this should behave. Got inspired by Final Fantasy VI.
        {
            transform.position += new Vector3(0,0.5f,0);
            transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce, 5f).OnComplete(() => StartCoroutine(WaitDestroy()));
        }

        public void SetValue(int value)
        {
            FloatingTextText.text = value.ToString();
            gameObject.SetActive(true);
        }

        public void ShowMessage(string message)
        {
            FloatingTextText.text = message;
            gameObject.SetActive(true);
        }

        private IEnumerator WaitDestroy()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}
