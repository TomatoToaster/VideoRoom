using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScreenAbsorb : MonoBehaviour
{
    public GameObject startingLinkedCam;
    public Transform intakePoint;

    private GameObject linkedCam;

    void Start()
    {
        linkedCam = startingLinkedCam;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        GameObject other = collisionInfo.gameObject;
        if (!other.CompareTag("Teleportable")) {
            return;
        }

        TeleportProjectile(other, collisionInfo.relativeVelocity);

    }

    public void LinkCam(GameObject cam)
    {
        linkedCam = cam;

        // Also link the cam back to this screen;
        linkedCam.GetComponent<PortaCam>().LinkScreen(gameObject);
    }

    private void TeleportProjectile(GameObject projectile, Vector3 intakeVelocity)
    {
        Debug.Log("Teleporting Projectile");

        // Method1
        // Parent the projectile to this screen, get relative position &
        // rotation and reparent to linked camera and apply the relative
        // rotation & position

        Transform teleportPoint = linkedCam.GetComponent<PortaCam>().GetTeleportPoint();

        projectile.transform.SetParent(intakePoint);
        projectile.transform.SetParent(teleportPoint, false);
        projectile.transform.SetParent(null);

        // Apply the rigidbodies velocity relative to the new orientation
        Rigidbody projRb = projectile.GetComponent<Rigidbody>();
        projRb.velocity = intakeVelocity.magnitude * projectile.transform.forward;



        // Method2
        // Calculate relative position from center using math of projectile and
        // change projectile location to linked camera position +
        // calculatedRelativePosition and apply rotation difference using quaterionions?
    }

}
