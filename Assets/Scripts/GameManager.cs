using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGame;

    public UnityEvent startGameEvent;

    private void Awake()
    {
        Application.targetFrameRate = 144;
    }

    private void Start()
    {
        isGame = false;
    }

    public void startGame()
    {
        isGame = true;

        startGameEvent.Invoke();
    }

    public void stopGame()
    {
        isGame = false;
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
