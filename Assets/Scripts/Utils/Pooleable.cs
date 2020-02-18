using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooleable : MonoBehaviour
{
    private bool isActive;

    public bool IsActive => isActive;
    
    /// <summary>
    /// Activates the Pooleable
    /// </summary>
    public virtual void Activate()
    {
        if(isActive) return;
        
        gameObject.SetActive(true);
        isActive = true;
    }

    /// <summary>
    /// Deactivates the Pooleable
    /// </summary>
    public virtual void Deactivate()
    {
        if (!isActive) return;
        
        gameObject.SetActive(false);
        isActive = false;
    }
}
