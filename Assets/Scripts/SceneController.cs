using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Fader fade;

    private void Start()
    {
        fade = FindObjectOfType<Fader>();
        if (fade == null)
        {
            Debug.LogError("Fader object not found in the scene.");
        }
    }

    public void PlayGame()
    {
        if (fade != null)
        {
            StartCoroutine(PlayGameCoroutine());
        }
        else
        {
            Debug.LogError("Fader object is not set.");
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator PlayGameCoroutine()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1); // Wait for the fade to complete
        SceneManager.LoadScene(1);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
