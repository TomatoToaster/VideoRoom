using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponSwap : MonoBehaviour
{
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject botLeft;
    public GameObject botRight;
    public float leftRightThreshold;
    public float headThreshold;
    public float hipThreshold;
    public bool isLeftHand;
    public GameObject handObject;

    private XRNode relevantControllerNode;
    private InputDevice handController;
    private InputDevice hmdController;
    private bool isHolding;
    private GameObject currentWeapon;


    // Start is called before the first frame update
    void Start()
    {
        isHolding = false;
        if (isLeftHand) {
            relevantControllerNode = XRNode.LeftHand;
        } else {
            relevantControllerNode = XRNode.RightHand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInputInitialized()) {
            return;
        }

        if(handController.TryGetFeatureValue(CommonUsages.gripButton, out bool isGripPressed)){
            HandleGrip(isGripPressed);
        };
    }

    private bool IsInputInitialized()
    {
        if (!handController.isValid) {
            handController = InputDevices.GetDeviceAtXRNode(relevantControllerNode);
            return false;
        }
        if (!hmdController.isValid) {
            hmdController = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            return false;
        }
        return true;
    }

    private void HandleGrip(bool isGripHeldThisFrame)
    {
        if (isHolding && !isGripHeldThisFrame) {
            UnequipWeapon();
        } else if (!isHolding && isGripHeldThisFrame) {
            EquipWeapon();
        }
    }

    // Actions to take when it's time to equip a weapon
    private void EquipWeapon()
    {
        // Conditionally grab the direction based on where the localPosition is;
        GameObject weapon = GetWeaponByLocation();
        if (weapon != null) {
            currentWeapon = Instantiate(weapon, gameObject.transform);
            SetHandVisibility(false);
            isHolding = true;
        }
    }

    // Drop the current weapon
    private void UnequipWeapon()
    {
        isHolding = false;
        if (!currentWeapon) {
            return;
        }
        handController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 throwVelocity);
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<DespawnWeapon>().Begin(throwVelocity);
        SetHandVisibility(true);
    }

    // Get wepaon based on relative hand controller rotation;
    private GameObject GetWeaponByLocation()
    {
        hmdController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 hmdPosition);
        handController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPosition);
        Vector3 relativePosition = handPosition - hmdPosition;
        Debug.Log(relativePosition);

        bool isLeftThreshold = relativePosition.x < -leftRightThreshold;
        bool isRightThreshold = relativePosition.x > leftRightThreshold;
        bool isTopThreshold = relativePosition.y > headThreshold;
        bool isBotThreshold = relativePosition.y < hipThreshold;

        if (isLeftThreshold && isTopThreshold) {
            return topLeft;
        } else if (isRightThreshold && isTopThreshold) {
            return topRight;
        } else if (isLeftThreshold && isBotThreshold) {
            return botLeft;
        } else if (isRightThreshold && isBotThreshold) {
            return botRight;
        }

        return null;
    }

    private void SetHandVisibility(bool shouldShow)
    {
        handObject.SetActive(shouldShow);
    }
}
