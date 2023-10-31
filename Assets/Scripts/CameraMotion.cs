using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class CameraMotion : MonoBehaviour
{
    public float speedHorizontal = 1;
    public float speedVertical = 1;
    public Vector2 rotationClamp;

    private InputDevice leftController;
    private InputDevice rightController;

    private Vector2 cameraAngleChange;
    private Vector3 originalRotation;

    void Start()
    {
        originalRotation = transform.rotation.eulerAngles;
        cameraAngleChange = Vector2.zero;
    }

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
    private void ApplyMotion(Vector2 controllerChange)
    {
        cameraAngleChange += new Vector2(controllerChange.x * speedHorizontal, controllerChange.y * speedVertical);
        cameraAngleChange = ClampXY(cameraAngleChange);

        transform.rotation = Quaternion.Euler(originalRotation + HandleXYToPitchYawRoll(cameraAngleChange));
    }

    // Turn reading from controller stick movement in terms of (x, y)
    // to Unity's 3D rotation (pitch, yaw, roll)
    // By default
    // Pitch should be inverted y from controller
    // Yaw is normal x from controller
    // Roll should never change
    private Vector3 HandleXYToPitchYawRoll(Vector2 controllerMove)
    {
        return new Vector3(-controllerMove.y, controllerMove.x, 0);
    }

    private Vector2 ClampXY(Vector2 vec)
    {
        float x = vec.x;
        float y = vec.y;

        x = Mathf.Clamp(x, -rotationClamp.x, rotationClamp.x);
        y = Mathf.Clamp(y, -rotationClamp.y, rotationClamp.y);
        return new Vector2(x, y);
    }
}
