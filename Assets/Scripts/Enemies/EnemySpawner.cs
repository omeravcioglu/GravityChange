using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LevelsManager levelsManager;

    public Transform[] spawnObjects;

    public GameObject[] enemies;
    [Space(15)]
    public float spawnTime;
    public float spawnRange;

    private Coroutine spawning;

    private void Start()
    {
        levelsManager = GetComponent<LevelsManager>();
    }

    public void startSpawnEnemies()
    {
        stopSpawnEnemies();
        spawning = StartCoroutine(spawnEnemy());
    }

    public void stopSpawnEnemies()
    {
        if (spawning != null) StopCoroutine(spawning);
    }

    public void deleteAllEnemies()
    {
        GameObject[] findedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] findedEnemyKilled = GameObject.FindGameObjectsWithTag("EnemyKilled");
        GameObject[] findedMines = GameObject.FindGameObjectsWithTag("Mine");

        foreach (GameObject enemy in findedEnemies)
        {
            Destroy(enemy);
        }

        foreach (GameObject enemy in findedEnemyKilled)
        {
            Destroy(enemy);
        }

        foreach (GameObject enemy in findedMines)
        {
            Destroy(enemy);
        }
    }

    private IEnumerator spawnEnemy()
    {
        while (GameManager.isGame)
        {
            Instantiate(enemies[getRandomEnemyNumber()],
                new Vector2(spawnObjects[Random.Range(0, 2)].position.x,
                Random.Range(-spawnRange, spawnRange)), Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private int getRandomEnemyNumber()
    {
        return Random.Range(0, levelsManager.level + 1 > enemies.Length ? enemies.Length : (levelsManager.level + 1));
    }
}
