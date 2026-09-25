using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroing : MonoBehaviour
{
    private static DontDestroing instance;

    private AudioSource music;

    void Start()
    {
        music = GetComponent<AudioSource>();

        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
            music.Play();
        }
    }
}
