using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Explode()
    {
        if (transform.parent) {
            transform.parent = null;
        }

        ps.Play();
    }
}
