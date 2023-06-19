using Services.Persistence;
using Services.SceneTransition;
using UnityEngine;

namespace UI.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private GameObject ContinueButton;

        private IPersistenceService PersistenceService;
    
        // Start is called before the first frame update
        void Start()
        {
            SceneTransitionService.CreateInstance();
            PersistenceService = new PlayerPrefsPersistenceService();

            if (!PersistenceService.HasUnitPoolData())
            {
                ContinueButton.SetActive(false);
            }
        }

        public void OnContinueClicked()
        {
            if (PersistenceService.HasStoredCombatData())
            {
                GoToBattle();
            }
            else
            {
                GoToHeroSelection();
            }
        }

        public void OnNewGameClicked()
        {
            PersistenceService.ClearPersistence();
            GoToHeroSelection();
        }

        private void GoToHeroSelection()
        {
            SceneTransitionService.Instance.TransitionTo(SceneIndexes.HeroSelection);
            //Nothing to send ahead to next scene since it's from lobby
        }

        private void GoToBattle()
        {
            SceneTransitionService.Instance.TransitionTo(SceneIndexes.Battle);
        }
    
    }
}
