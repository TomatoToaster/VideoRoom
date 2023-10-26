using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool shouldBeOpen;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float splitDifference;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        shouldBeOpen = false;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldBeOpen != isOpen) {
            toggleDoors();
        }
    }

    private void toggleDoors() {
        if (isOpen) {
            leftDoor.transform.localPosition += splitDifference * Vector3.left;
            rightDoor.transform.localPosition += splitDifference * Vector3.right;
        } else {
            leftDoor.transform.localPosition -= splitDifference * Vector3.left;
            rightDoor.transform.localPosition -= splitDifference * Vector3.right;
        }
        isOpen = !isOpen;
    }
}
