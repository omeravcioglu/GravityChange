using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public Text levelText;
    public Animator newLevelAnimator;
    [Space(15)]
    public int level;
    public UnityEvent newLevelEvent;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            level = 0;
            PlayerPrefs.SetInt("Level", level);
        }

        levelText.text = "Level: " + level.ToString();
    }

    public bool checkOnNewLevel(int points)
    {
        if (points >= needToNextLevel())
        {
            level++;
            saveLevel();
            showNewLevelAnimation();
            newLevelEvent.Invoke();

            updateLevelText();

            return true;
        }

        return false;
    }

    public int needToNextLevel()
    {
        return (level + 1) * 20;
    }

    public void updateLevelText()
    {
        levelText.text = "Level: " + level.ToString();
    }

    public void showNewLevelAnimation()
    {
        StartCoroutine(newLevelShowing());
    }

    private void saveLevel()
    {
        PlayerPrefs.SetInt("Level", level);
    }

    private IEnumerator newLevelShowing()
    {
        newLevelAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(newLevelAnimator.GetCurrentAnimatorStateInfo(0).length);
        newLevelAnimator.gameObject.SetActive(false);
    }
}
