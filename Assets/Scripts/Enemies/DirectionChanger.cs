using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class DirectionChanger : MonoBehaviour
{
    private Transform enemyTransform;
    private RandomPlaceForEnemy randomPlaceForEnemy;
    private EnemyMovement enemyMovement;

    private Vector2 directionToChange;
    public float changeTime;


    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();
        enemyMovement = GetComponent<EnemyMovement>();

        directionToChange = Random.Range(0, 2) == 0 ? Vector2.up : Vector2.down;

        StartCoroutine(changeDirectionCoroutine());
    }

    private IEnumerator changeDirectionCoroutine()
    {
        yield return new WaitUntil(() => randomPlaceForEnemy.progress == 1f);

        float time = 0;
        Vector2 startDirection = enemyMovement.direction;

        while (time < changeTime)
        {
            enemyMovement.direction = Vector2.Lerp(startDirection, directionToChange, time / changeTime);
            time += Time.deltaTime;
            yield return null;
        }

        enemyMovement.direction = directionToChange;
    }
}
