using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWeapon : MonoBehaviour
{
    public float destroyTime = 2;
    public float throwFactor = 1;
    private Rigidbody rb;
    private float timeElapsed;
    private bool isDespawning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeElapsed = 0;
        isDespawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDespawning) {
            return;
        }

        if (timeElapsed < destroyTime) {
            timeElapsed += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }

    }

    public void Begin(Vector3 throwVelocity)
    {
        isDespawning = true;
        rb.isKinematic = false;
        rb.velocity = throwVelocity * throwFactor;
    }
}
