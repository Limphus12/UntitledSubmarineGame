using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private UnityEvent loadSceneEvent;

    [SerializeField] private Animator fadeToBlackAnimator;
    [SerializeField] private float fadeTime;

    public void LoadScene(int scene)
    {
        fadeToBlackAnimator.SetTrigger("fadeTo");

        loadSceneEvent?.Invoke();

        StartCoroutine(LoadSceneFade(scene));
    }

    public void LoadScene(string scene)
    {
        fadeToBlackAnimator.SetTrigger("fadeTo");

        loadSceneEvent?.Invoke();

        StartCoroutine(LoadSceneFade(scene));
    }

    private IEnumerator LoadSceneFade(int scene)
    {
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadSceneAsync(scene);
    }

    private IEnumerator LoadSceneFade(string scene)
    {
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadSceneAsync(scene);
    }
}