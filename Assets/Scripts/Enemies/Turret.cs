using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class Turret : MonoBehaviour
{
    private Transform enemyTransform;
    private RandomPlaceForEnemy randomPlaceForEnemy;
    private EnemyMovement enemyMovement;
    private Transform heroTransform;

    public GameObject bullet;   

    public float rotateSpeed;
    public float cooldown;

    private bool canShoot;
    private float currentCooldown;

    private Vector2 targetDestination;
    private float targetRotation;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();
        enemyMovement = GetComponent<EnemyMovement>();
        heroTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        canShoot = false;
        currentCooldown = 0;

        StartCoroutine(turretCoroutine());
    }

    private void FixedUpdate()
    {
        if (canShoot && GameManager.isGame)
        {
            targetDestination = heroTransform.position - enemyTransform.position;
            targetRotation = Mathf.Atan2(targetDestination.y, targetDestination.x) * Mathf.Rad2Deg;
            enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, Quaternion.AngleAxis(targetRotation, Vector3.forward), rotateSpeed * Time.fixedDeltaTime);

            if (currentCooldown > cooldown)
            {
                currentCooldown = 0;
                shoot();
            }

            currentCooldown += Time.fixedDeltaTime;
        }
    }

    private void shoot()
    {
        GameObject bul = Instantiate(bullet, enemyTransform.position, Quaternion.identity);
        bul.GetComponent<BulletMovement>().startDirection = enemyTransform.TransformVector(Vector2.right);
        AudioManager.instance.playSound(Sounds.shoot);
    }

    private IEnumerator turretCoroutine()
    {
        yield return new WaitUntil(() => randomPlaceForEnemy.progress == 1f);

        enemyMovement.rotateSpeed = 0;
        enemyMovement.speed = 0;

        canShoot = true;
    }
}
