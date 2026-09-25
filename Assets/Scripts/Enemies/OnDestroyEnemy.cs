using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyEnemy : MonoBehaviour
{
    public ParticleSystem particle;
    public Color color;

    public void OnDestroy()
    {
        particle.startColor = color;
        Instantiate(particle.gameObject, transform.position, Quaternion.identity);
    }
}
