using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private IEnumerator LoadGameplayLevel(int sceneIndex)
    {
        anim.Play("SCENELOADER_FADEIN");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        yield return new WaitForSeconds(1.75f);
    }


    #region Message Receivers
    public void LoadMainMenu()
    {
        StartCoroutine(LoadGameplayLevel(0));
    }

    public void LoadPreGameplay()
    {
        StartCoroutine(LoadGameplayLevel(1));
    }

    public void LoadGameplay()
    {
        StartCoroutine(LoadGameplayLevel(2));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameplayLevel(3));
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadGameplayLevel(sceneIndex));
    }
    #endregion
}
