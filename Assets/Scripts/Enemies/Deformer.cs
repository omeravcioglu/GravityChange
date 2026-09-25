using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class Deformer : MonoBehaviour
{
    private Transform enemyTransform;
    private RandomPlaceForEnemy randomPlaceForEnemy;

    public Vector2 scaleToDeform;
    public float deformTime;

    
    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();

        StartCoroutine(deformCoroutine());
    }

    private IEnumerator deformCoroutine()
    {
        yield return new WaitUntil(() => randomPlaceForEnemy.progress == 1f);

        float time = 0;
        Vector2 startScale = enemyTransform.localScale;

        while (time < deformTime)
        {
            enemyTransform.localScale = Vector2.Lerp(startScale, scaleToDeform, time / deformTime);
            time += Time.deltaTime;
            yield return null;
        }

        enemyTransform.localScale = scaleToDeform;
    }
}
