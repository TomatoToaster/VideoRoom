using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaCam : MonoBehaviour
{
    public Transform spawnPoint;

    private GameObject linkedScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LinkScreen(GameObject screen)
    {
        linkedScreen = screen;
    }

    public Transform GetTeleportPoint()
    {
        return spawnPoint;
    }
}
