using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponInputManager : MonoBehaviour
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
    private bool isHoldingGrip;
    private bool isHoldingTrigger;
    private GameObject currentWeapon;


    // Start is called before the first frame update
    void Start()
    {
        isHoldingGrip = false;
        isHoldingTrigger = false;
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

        if(handController.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed)){
            HandleTrigger(isTriggerPressed);
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
        if (!isHoldingGrip && isGripHeldThisFrame) {
            EquipWeapon();
            isHoldingGrip = true;
        } else if (isHoldingGrip && !isGripHeldThisFrame) {
            UnequipWeapon();
            isHoldingGrip = false;
        }
    }

    private void HandleTrigger(bool isTriggerHeldThisFrame)
    {
        if (!currentWeapon) {
            return;
        }

        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        if (!isHoldingTrigger && isTriggerHeldThisFrame) {
            weapon.Activate();
            isHoldingTrigger = true;
        } else if (isHoldingTrigger && !isTriggerHeldThisFrame) {
            weapon.Deactivate();
            isHoldingTrigger = false;
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
        }
    }

    // Drop the current weapon
    private void UnequipWeapon()
    {
        isHoldingGrip = false;
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
