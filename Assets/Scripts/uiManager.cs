using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    public GameObject userControls;
    public Joystick joystick;
    [Space(15)]
    public Slider masterSlider;
    public Slider soundsSlider;
    public Slider musicSlider;
    [Space(15)]
    public Animator letsGoTextAnimator;
    [Space(15)]
    public UnityEvent startGameUI;
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterSlider"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterSlider");
        }
        else
        {
            masterSlider.value = masterSlider.maxValue;
            PlayerPrefs.SetFloat("MasterSlider", masterSlider.value);
        }

        if (PlayerPrefs.HasKey("SoundsSlider"))
        {
            soundsSlider.value = PlayerPrefs.GetFloat("SoundsSlider");
        }
        else
        {
            soundsSlider.value = soundsSlider.maxValue;
            PlayerPrefs.SetFloat("SoundsSlider", soundsSlider.value);
        }

        if (PlayerPrefs.HasKey("MusicSlider"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicSlider");
        }
        else
        {
            musicSlider.value = musicSlider.maxValue;
            PlayerPrefs.SetFloat("MusicSlider", musicSlider.value);
        }
    }

    public void disableUserControls()
    {
        userControls.SetActive(true);
    }

    public void enableUserControls()
    {
        userControls.SetActive(false);
    }

    public void saveMasterSlider()
    {
        PlayerPrefs.SetFloat("MasterSlider", masterSlider.value);
    }

    public void saveSoundsSlider()
    {
        PlayerPrefs.SetFloat("SoundsSlider", soundsSlider.value);
    }

    public void saveMusicSlider()
    {
        PlayerPrefs.SetFloat("MusicSlider", musicSlider.value);
    } 

    public void startGame()
    {
        startGameUI.Invoke();
    }

    public void showLetsGoText()
    {
        StartCoroutine(letsGoShowing());
    }

    private IEnumerator letsGoShowing()
    {
        letsGoTextAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(letsGoTextAnimator.GetCurrentAnimatorStateInfo(0).length);
        letsGoTextAnimator.gameObject.SetActive(false);
    }
}
