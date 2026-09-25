using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float timeToExplode = 3;

    private void Start()
    {
        StartCoroutine(explodeTimer());
    }

    private IEnumerator explodeTimer()
    {
        yield return new WaitForSeconds(timeToExplode);

        AudioManager.instance.playSound(Sounds.explode);
        Destroy(gameObject);
    }
}
