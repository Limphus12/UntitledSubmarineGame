using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace com.limphus.utilities
{
    public class ASyncLoader : MonoBehaviour
    {
        [Header("Menu Screens")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject menu;

        [Header("Slider")]
        [SerializeField] private Slider progressBar;

        public void LoadLevelBtn(string levelToLoad)
        {
            menu.SetActive(false);
            loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelAsync(levelToLoad));
        }

        public void LoadLevelBtn(int levelToLoad)
        {
            menu.SetActive(false);
            loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelAsync(levelToLoad));
        }

        private IEnumerator LoadLevelAsync(string levelToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                progressBar.value = progressValue;
                yield return null;
            }
        }

        private IEnumerator LoadLevelAsync(int levelToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                progressBar.value = progressValue;
                yield return null;
            }
        }
    }
}