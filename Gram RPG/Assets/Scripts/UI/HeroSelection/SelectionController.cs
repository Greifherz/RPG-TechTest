using System.Linq;
using Services.Persistence;
using Services.SceneTransition;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroSelection
{
    public class SelectionController : MonoBehaviour
    {
        [SerializeField] private SelectionPanel SelectionPanel;
        [SerializeField] private Button ToBattleButton;

        private UnitPool UnitPool;
        private IPersistenceService PersistenceService;
        private int[] SelectedHeroes;

        private void Start()
        {
            PersistenceService = new PlayerPrefsPersistenceService();
            UnitPool = PersistenceService.LoadUnitPool();
        
            SelectedHeroes = new int[] {
                -1,-1,-1
            };
            SelectionPanel.SetUnitPoolData(UnitPool);
        }

        public void Select(int index)
        {
            if (SelectedHeroes.Contains(index))
            {
                for (int i = 0; i < SelectedHeroes.Length; i++)
                {
                    if (SelectedHeroes[i] == index)
                    {
                        SelectedHeroes[i] = -1;
                        SelectionPanel.Unselect(index);
                    }
                }
            }
            else
            {
                var CanSelect = SelectedHeroes.Any(x => x == -1);
                if (CanSelect)
                {
                    for (int i = 0; i < SelectedHeroes.Length; i++)
                    {
                        if (SelectedHeroes[i] == -1)
                        {
                            SelectedHeroes[i] = index;   
                            break;
                        }
                    }
                }
                else
                {
                    var ToUnselect = SelectedHeroes[0];
                    for (int i = 0; i < SelectedHeroes.Length - 1; i++)
                    {
                        SelectedHeroes[i] = SelectedHeroes[i + 1];
                    }

                    SelectedHeroes[SelectedHeroes.Length - 1] = index;
                    SelectionPanel.Unselect(ToUnselect);
                }
            }

            ToBattleButton.interactable = SelectedHeroes.All(x => x != -1);
        }

        public void GoToBattle()
        {
            UnitPool.SelectedUnits = SelectedHeroes;
            PersistenceService.SaveUnitPool(UnitPool);

        
            SceneTransitionService.Instance.TransitionTo(SceneIndexes.Battle); //All communication between scenes shall take place through persistence.
            //If the game data is too big, later on, it might be a good idea to have a different approach, such as having a "master" scene or just letting the dependency injector handle it
        }
    }
}
