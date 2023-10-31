using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Weapon
{
    public GameObject projectilePrefab;
    public float cooldown;
    public Transform shootOrigin;

    private float remainingCD;

    protected new void Start()
    {
        base.Start();
        remainingCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingCD > 0) {
            remainingCD -= Time.deltaTime;
            return;
        }

        if (IsActive()) {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (shootOrigin) {
            Instantiate(projectilePrefab, shootOrigin.position, shootOrigin.rotation);
        } else {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
        remainingCD = cooldown;
    }
}
