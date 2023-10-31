using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public float lifeTime = 1;
    public DeathParticles deathParticles;

    private Rigidbody rb;
    private float lifeLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * moveSpeed;
        lifeLeft = lifeTime;
    }

    void Update()
    {
        if (lifeLeft > 0) {
            lifeLeft -= Time.deltaTime;
        } else {
            DestroySelf();
        }
    }

    // void OnCollisionEnter(Collision collisionInfo)
    // {
    //     DestroySelf();
    // }

    public void DestroySelf()
    {
        deathParticles.Explode();
        Destroy(gameObject);
    }


    // TODO delete
    // void FixedUpdate()
    // {
    //     Debug.Log(rb.velocity);
    // }

}
