using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform enemyTransform;

    public float speed;
    public float rotateSpeed;

    public Vector2 direction;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();

        direction = enemyTransform.position.x > 0 ? Vector2.left : Vector2.right;
    }

    private void FixedUpdate()
    {
        if (GameManager.isGame)
        {
            enemyTransform.Translate(enemyTransform.InverseTransformDirection(direction) * speed * Time.fixedDeltaTime);
            enemyTransform.rotation = enemyTransform.rotation * Quaternion.AngleAxis(rotateSpeed, Vector3.back);
        }
    }
}
