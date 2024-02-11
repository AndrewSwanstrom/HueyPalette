using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    Fader fade;
    void Start()
    {
        //fade = FindObjectOfType<Fader>();

        //fade.FadeOut();
    }
    private void OnTriggerEnter2D(Collider2D collision)


    {
        if (collision.tag == "Player")
        {
            {
                if (fade != null)
                {
                    StartCoroutine(NextLevelCoroutine());
                }
                else
                {
                    Debug.LogError("Fader object is not set.");
                    SceneManager.LoadScene(2);
                }
            }

        }
    }
    private IEnumerator NextLevelCoroutine()
    {
        //fade.FadeIn();
        yield return new WaitForSeconds(1); // Wait for the fade to complete
        SceneManager.LoadScene(2);
    }
}