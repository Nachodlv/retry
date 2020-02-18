using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooleable : MonoBehaviour
{
    private ObjectPooler objectPooler;
    private bool isActive;

    /// <summary>
    /// Activates the game object.
    /// </summary>
    /// <param name="pooleable">ObjectPooler that will be used when this GameObject is deactivated</param>
    public virtual void Activate(ObjectPooler pooleable)
    {
        if(isActive) return;
        
        gameObject.SetActive(true);
        objectPooler = pooleable;
        isActive = true;
    }

    /// <summary>
    /// Deactivated the game object. Tells the object pooler that this game object is deactivated.
    /// </summary>
    public virtual void Deactivate()
    {
        if (!isActive) return;
        
        gameObject.SetActive(false);
        objectPooler?.PooleableDeactivated(this);
        isActive = false;
    }
}
