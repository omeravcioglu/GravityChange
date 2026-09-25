using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Transform backTransform;
    private Rigidbody2D heroRigibody;

    public Joystick joystick;
    public HeroController heroController;
    public float speed;

    private void Start()
    {
        backTransform = GetComponent<Transform>();
        heroRigibody = heroController.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameManager.isGame && !heroController.isBorder)
        {
            backTransform.Translate((joystick.Horizontal * Vector2.left - heroRigibody.velocity.normalized) * speed * Time.fixedDeltaTime);
        }
    }
}
