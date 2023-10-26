using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraMotion : MonoBehaviour
{
    public float speedHorizontal = 1;
    public float speedVertical = 1;

    private InputDevice leftController;
    private InputDevice rightController;

    // Update is called once per frame
    void Update()
    {
        if (!IsInputInitialized()) {
            return;
        }
        Vector2 camMove = GetCameraMove();
        ApplyMotion(camMove);
    }

    private Vector2 GetCameraMove()
    {
        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftMove);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightMove);

        return leftMove.sqrMagnitude > rightMove.sqrMagnitude ? leftMove : rightMove;
    }

    private bool IsInputInitialized()
    {
        if (!leftController.isValid) {
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            return false;
        }
        if (!rightController.isValid) {
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            return false;
        }
        return true;
    }
    private void ApplyMotion(Vector2 move)
    {
        transform.Rotate(Vector3.left * move.y * speedHorizontal);
        transform.Rotate(Vector3.up * move.x * speedVertical);
    }
}
