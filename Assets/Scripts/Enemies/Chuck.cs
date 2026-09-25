using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class Chuck : MonoBehaviour
{
    private Transform enemyTransform;
    private RandomPlaceForEnemy randomPlaceForEnemy;
    private EnemyMovement enemyMovement;

    public float scaleSpeedToChange;
    public float changeTime;


    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();
        enemyMovement = GetComponent<EnemyMovement>();

        StartCoroutine(deformCoroutine());
    }

    private IEnumerator deformCoroutine()
    {
        yield return new WaitUntil(() => randomPlaceForEnemy.progress == 1f);

        float time = 0;
        float startSpeed = enemyMovement.speed;

        while (time < changeTime)
        {
            enemyMovement.speed = Mathf.Lerp(startSpeed, scaleSpeedToChange, time / changeTime);
            time += Time.deltaTime;
            yield return null;
        }

        enemyMovement.speed = scaleSpeedToChange;
    }
}
