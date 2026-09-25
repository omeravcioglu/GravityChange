using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroController : MonoBehaviour
{
    private Transform heroTransform;
    private Rigidbody2D heroRigidBody;

    public Joystick joystick;
    public ParticleSystem deadParticle;
    [Space(15)]
    public int speed;
    public float horizontalSpeed;
    [Space(15)]
    public float adDelay;
    [Space(15)]
    public UnityEvent collisionWithPlatformEvent;
    public UnityEvent looseEvent;

    private bool isGround;
    [HideInInspector] public bool isBorder;

    private Vector2 direction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            isGround = true;

            if (GameManager.isGame)
            {
                collisionWithPlatformEvent.Invoke();
            }
        }

        if (collision.transform.CompareTag("Border"))
        {
            isBorder = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            isGround = false;
        }

        if (collision.transform.CompareTag("Border"))
        {
            isBorder = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.isGame)
        {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("You loose");
                looseEvent.Invoke();                
            }

            if (collision.CompareTag("EnemyKilled"))
            {
                AudioManager.instance.playSound(Sounds.explode);
                Destroy(collision.gameObject);
            }

            if (collision.CompareTag("Mine"))
            {
                AudioManager.instance.playSound(Sounds.explode);
                Destroy(collision.gameObject);
                looseEvent.Invoke();
            }
        }
    }

    private void Start()
    {
        heroTransform = GetComponent<Transform>();
        heroRigidBody = GetComponent<Rigidbody2D>();

        heroRigidBody.gravityScale = speed;

        isGround = true;
        direction = Vector2.zero;
    }

    private void Update()
    {
        if (GameManager.isGame)
        {
            //Computer
            if (Input.GetKeyUp(KeyCode.Space))
            {
                changeGravity();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                direction.x++;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                direction.x--;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                direction.x--;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                direction.x++;
            }

            direction.x = joystick.Horizontal;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.isGame)
        {
            //heroRigidBody.AddForce(direction * horizontalSpeed);
            heroTransform.Translate(direction * horizontalSpeed * Time.fixedDeltaTime);
        }
    }

    public void looseHero()
    {
        deadParticle.Play();

        AudioManager.instance.playSound(Sounds.loss);
        AdManager.instance.showAd(adDelay);
    }

    public void changeGravity()
    {
        if (isGround)
        {
            heroRigidBody.gravityScale = heroRigidBody.gravityScale > 0 ? -speed : speed;
            AudioManager.instance.playSound(Sounds.jump);
        }
    }
}
