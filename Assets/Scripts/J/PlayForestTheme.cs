using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayForestTheme : MonoBehaviour
{
    public AudioSource src;
    public AudioClip PlayThemeClip;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.volume = 0.7f;
        src.clip = PlayThemeClip;
        src.Play();
    }
}
