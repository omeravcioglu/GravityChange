using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;
    [Space(15)]
    public AudioSource buttonSound;
    public AudioSource explodeSound;
    public AudioSource jumpSound;
    public AudioSource shootSound;
    public AudioSource lossSound;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public void playSound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.button:
                buttonSound.Play();
                break;
            case Sounds.explode:
                explodeSound.Play();
                break;
            case Sounds.jump:
                jumpSound.Play();
                break;
            case Sounds.shoot:
                shootSound.Play();
                break;
            case Sounds.loss:
                lossSound.Play();
                break;
        }
    }

    public void playButtonSound()
    {
        playSound(Sounds.button);
    }

    public void setSoundVolume(Slider slider)
    {
        audioMixer.SetFloat("soundVolume", Mathf.Lerp(-80, 0, slider.value));
    }

    public void setMusicVolume(Slider slider)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80, 0, slider.value));
    }

    public void setMasterVolume(Slider slider)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Lerp(-80, 0, slider.value));
    }
}

public enum Sounds
{
    button,
    explode,
    jump,
    shoot,
    loss
}
