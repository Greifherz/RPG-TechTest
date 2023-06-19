using Unit;
using UnityEngine;

namespace UI.HeroSelection
{
    public class SelectionPanel : MonoBehaviour
    {
        [SerializeField] private UnitView[] UnitSelections;

        public void SetUnitPoolData(UnitPool pool)
        {
            for (int i = 0; i < UnitSelections.Length; i++)
            {
                if (pool.UnitsInPool.Count > i)
                {
                    UnitSelections[i].SetUnitData(pool.UnitsInPool[i]);
                }
                else
                {
                    UnitSelections[i].gameObject.SetActive(false);
                }
            }
        }

        public void Unselect(int index)
        {
            UnitSelections[index].Unselect();
        }
    }
}
