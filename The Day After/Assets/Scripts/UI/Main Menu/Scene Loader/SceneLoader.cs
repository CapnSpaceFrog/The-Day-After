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
        yield return new WaitForSeconds(2.25f);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
    }


    #region Message Receivers
    public void LoadMainMenu()
    {
        StartCoroutine(LoadGameplayLevel(0));
    }

    public void LoadGameplay()
    {
        StartCoroutine(LoadGameplayLevel(1));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameplayLevel(2));
    }
    #endregion
}
