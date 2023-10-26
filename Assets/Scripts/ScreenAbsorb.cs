using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAbsorb : MonoBehaviour
{
    public GameObject linkedCam;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "projectile") {
            return;
        }

        Vector3 inPos = other.transform.position;
        Vector3 inRot = other.transform.rotation.eulerAngles;
    }
}
