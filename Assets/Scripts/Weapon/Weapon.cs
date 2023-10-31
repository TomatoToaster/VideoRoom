using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private bool isActive;

    // Start is called before the first frame update
    protected void Start()
    {
        isActive = false;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    protected bool IsActive()
    {
        return isActive;
    }
}
