using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class Miner : MonoBehaviour
{
    private Transform enemyTransform;
    private EnemyMovement enemyMovement;
    private RandomPlaceForEnemy randomPlaceForEnemy;

    public GameObject mine;

    [Range(1f, 3f)] public float minCooldown;
    [Range(4f, 6f)] public float maxCooldown;

    private float cooldown;
    private float currentCooldown;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();
        enemyMovement = GetComponent<EnemyMovement>();

        cooldown = Random.Range(minCooldown, maxCooldown);
        currentCooldown = 0;
    }

    private void FixedUpdate()
    {
        if (randomPlaceForEnemy.inRange(enemyTransform.position) && GameManager.isGame)
        {
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
        GameObject min = Instantiate(mine, enemyTransform.position, Quaternion.identity);
        min.GetComponent<Mine>().timeToExplode = cooldown + 1;
        AudioManager.instance.playSound(Sounds.shoot);
    }
}
