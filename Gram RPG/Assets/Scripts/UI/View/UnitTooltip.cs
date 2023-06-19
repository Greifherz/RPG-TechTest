using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class UnitTooltip : MonoBehaviour
    {
        [SerializeField] private Text NameText;
        [SerializeField] private Text HealthText;
        [SerializeField] private Text AttackPowerText;
        [SerializeField] private Text ExperienceText;

        public void SetUnitData(UnitData dataToShow)
        {
            NameText.text = dataToShow.Name;
            HealthText.text = "HP: " + dataToShow.Health + " / " + dataToShow.MaxHealth;
            AttackPowerText.text = "Attack Power: " + dataToShow.AttackPower;
            ExperienceText.text = "Experience: " + dataToShow.Experience;
        }

        public void Show(Vector3 callerPos)
        {
            transform.position = callerPos + new Vector3(0, 0.5f, 0);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
