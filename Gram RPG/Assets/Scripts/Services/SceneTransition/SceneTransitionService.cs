using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services.SceneTransition
{
    public class SceneTransitionService : MonoBehaviour, ISceneTransitionService
    {
        [SerializeField] private Image FadePanel;
        [SerializeField] private int CurrentSceneIndex = 0;
    
        public static SceneTransitionService Instance; //Again another singleton :( this time around worse. It's hard not to singleton-nize everything without a good dependency injection.
    
        private void Start()
        {
            Instance = this;
        }

        public static void CreateInstance()
        {
            SceneManager.LoadScene(1,LoadSceneMode.Additive);
        }

        public void TransitionTo(SceneIndexes sceneIndex)
        {
            FadePanel.raycastTarget = true;
            FadePanel.DOFade(1, 0.5f).OnComplete(() =>
            {
                SceneManager.UnloadSceneAsync(CurrentSceneIndex);
                SceneManager.LoadSceneAsync((int)sceneIndex,LoadSceneMode.Additive).completed += (op) =>
                {
                    CurrentSceneIndex = (int)sceneIndex;
                    FadePanel.DOFade(0, 0.5f).OnComplete(() =>
                    {
                        FadePanel.raycastTarget = false;
                    });
                };
            });
        }

    }

    public enum SceneIndexes
    {
        Lobby = 0,
        HeroSelection = 2,
        Battle = 3
    }
}