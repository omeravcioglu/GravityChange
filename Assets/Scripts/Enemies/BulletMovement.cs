using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Transform bulletTransform;

    public Vector2 startDirection;
    public float speed;
    public float timeAlive;

    private float currentTimeAlive;

    private void Start()
    {
        bulletTransform = GetComponent<Transform>();

        currentTimeAlive = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.isGame)
        {
            bulletTransform.Translate(startDirection * speed * Time.fixedDeltaTime);

            currentTimeAlive += Time.fixedDeltaTime;
            if (currentTimeAlive > timeAlive) Destroy(gameObject);
        }
    }
}
