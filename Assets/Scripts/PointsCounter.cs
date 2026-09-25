using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    private LevelsManager levelsManager;

    public Text pointsOutText;
    [Space(15)]
    public int points;
    public int toAddPoints;

    private void Start()
    {
        levelsManager = GetComponent<LevelsManager>();

        drawPoints();
    }

    public void addPoints()
    {
        points += toAddPoints;

        if (levelsManager.checkOnNewLevel(points))
        {
            points = 0;
        }

        drawPoints();
    }

    public void refreshPoints()
    {
        points = 0;
        drawPoints();
    }

    private void drawPoints()
    {
        pointsOutText.text = points.ToString() + " / " + levelsManager.needToNextLevel().ToString();
    }
}
