using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAbsorb : MonoBehaviour
{
    public GameObject linkedCam;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("projectile")) {
            return;
        }
        Vector3 inPos = other.transform.position;
        Vector3 inRot = other.transform.rotation.eulerAngles;

        // Method1
        // Parent the projectile to this screen, get relative position &
        // rotation and reparent to linked camera and apply the relative
        // rotation & position

        // Method2
        // Calculate relative position from center using math of projectile and
        // change projectile location to linked camera position +
        // calculatedRelativePosition and apply rotation difference using quaterionions?
    }

    public void ChangeLinkedCam(GameObject obj)
    {
        linkedCam = obj;
    }
}
