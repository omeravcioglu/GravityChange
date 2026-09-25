using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaceForEnemy : MonoBehaviour
{
    private Transform enemyTransform;

    [HideInInspector] public float xtoDeform;
    [HideInInspector] public bool toLess;

    [Range(0f, 7f)] public float xrange;
    [Space(15)]
    public float progress;

    private float distance;
    private bool progressCanChange;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();

        progress = 0f;
        progressCanChange = true;

        xtoDeform = Random.Range(-xrange, xrange);

        if (enemyTransform.position.x > 0) toLess = true;
        else toLess = false;

        distance = toLess ? (enemyTransform.position.x - xtoDeform) : (xtoDeform - enemyTransform.position.x);

        StartCoroutine(randomPlaceIsEnabled());
    }

    private void Update()
    {
        if (progressCanChange) progress = (distance - (toLess ? (enemyTransform.position.x - xtoDeform) : (xtoDeform - enemyTransform.position.x))) / distance;
    }

    public bool inRange(Vector2 pos)
    {
        return (pos.x > -xrange && pos.x < xrange);
    }

    private IEnumerator randomPlaceIsEnabled()
    {
        if (toLess)
        {
            yield return new WaitWhile(() => enemyTransform.position.x > xtoDeform);
        }
        else
        {
            yield return new WaitWhile(() => enemyTransform.position.x < xtoDeform);
        }

        progressCanChange = false;
        progress = 1f;
    }
}
