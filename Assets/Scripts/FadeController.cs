using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    Fader fade;

    void Start()
    {
        fade = FindObjectOfType<Fader>();

        fade.FadeOut();
    }
}
