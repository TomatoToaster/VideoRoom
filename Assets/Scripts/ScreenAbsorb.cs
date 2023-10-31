using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScreenAbsorb : MonoBehaviour
{
    public GameObject startingLinkedCam;

    private GameObject linkedCam;

    void Start()
    {
        linkedCam = startingLinkedCam;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        GameObject other = collisionInfo.gameObject;
        if (!other.CompareTag("Projectile")) {
            return;
        }
        Debug.Log(collisionInfo.relativeVelocity);
        Debug.Log(other.GetComponent<Rigidbody>().velocity);

        TeleportProjectile(other);

    }

    public void LinkCam(GameObject cam)
    {
        linkedCam = cam;

        // Also link the cam back to this screen;
        linkedCam.GetComponent<PortaCam>().LinkScreen(gameObject);
    }

    private void TeleportProjectile(GameObject projectile)
    {
        Debug.Log("Teleporting Projectile");

        // Method1
        // Parent the projectile to this screen, get relative position &
        // rotation and reparent to linked camera and apply the relative
        // rotation & position

        // Transform teleportPoint = linkedCam.GetComponent<PortaCam>().GetTeleportPoint();

        // projectile.transform.parent = transform;
        // projectile.transform.GetLocalPositionAndRotation(out Vector3 locPos, out Quaternion locRot);

        // projectile.transform.parent = teleportPoint;
        // projectile.transform.SetLocalPositionAndRotation(locPos, locRot);

        // projectile.transform.parent = null;

        // // Apply the rigidbodies velocity to the new orientation
        // Rigidbody projRb = projectile.GetComponent<Rigidbody>();
        // projRb.velocity = projRb.velocity.magnitude * projectile.transform.forward;

        // Attempt 2


        // Method2
        // Calculate relative position from center using math of projectile and
        // change projectile location to linked camera position +
        // calculatedRelativePosition and apply rotation difference using quaterionions?
    }

}
