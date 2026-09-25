using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomPlaceForEnemy))]
public class Exploser : MonoBehaviour
{
    private Transform enemyTransform;
    private RandomPlaceForEnemy randomPlaceForEnemy;

    public GameObject bullet;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        randomPlaceForEnemy = GetComponent<RandomPlaceForEnemy>();

        StartCoroutine(exploseCoroutine());
    }

    private IEnumerator exploseCoroutine()
    {
        yield return new WaitUntil(() => randomPlaceForEnemy.progress == 1f);

        GameObject bul;

        for (int i = 0; i < 4; i++)
        {
            bul = Instantiate(bullet, enemyTransform.position, Quaternion.identity);
            bul.GetComponent<BulletMovement>().startDirection = i == 0 ? Vector2.up : (i == 1 ? Vector2.right : (i == 2 ? Vector2.down : Vector2.left));
        }

        AudioManager.instance.playSound(Sounds.explode);
        Destroy(gameObject);
    }
}
