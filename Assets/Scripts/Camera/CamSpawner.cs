using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSpawner : MonoBehaviour
{
    public GameObject cam;

    public bool onlyActivateAfterScreen;

    private bool hasGoneThroughScreen;

    void Start()
    {
        hasGoneThroughScreen = false;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (onlyActivateAfterScreen && !hasGoneThroughScreen) {
            return;
        }

        // Don't spawn on screens
        if (collisionInfo.gameObject.CompareTag("Screen")) {
            return;
        }

        ContactPoint contactPoint = collisionInfo.GetContact(0);
        Vector3 pos = contactPoint.point;
        Quaternion rot = Quaternion.LookRotation(contactPoint.normal, Vector3.up);
        GameObject newCam = Instantiate(cam, pos, rot);

        // For now always replace the main screen
        GameObject.FindWithTag("GameController").GetComponent<CameraManager>().ReplaceCamAtScreen(0, newCam);
        Destroy(gameObject);
    }

    public void FlagGoingThroughScreen()
    {
        hasGoneThroughScreen = true;
    }
}
