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
    private IEnumerator LoadGameplayLevel()
    {
        anim.Play("SCENELOADER_FADEIN");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Single);
        yield return new WaitForSeconds(2f);
    }


    #region Message Receivers
    private void LoadGameplay()
    {
        StartCoroutine(LoadGameplayLevel());
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void LoadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
    #endregion
}
