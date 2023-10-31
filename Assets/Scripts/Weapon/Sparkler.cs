using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkler : Weapon
{
    public GameObject particleHolder;
    private ParticleSystem ps;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ps = particleHolder.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive() && ps.isPlaying) {
            ps.Stop();
        } else if (IsActive() && !ps.isPlaying) {
            ps.Play();
        }
    }
}
