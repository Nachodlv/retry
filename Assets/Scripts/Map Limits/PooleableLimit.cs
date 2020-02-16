using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deactivates all pooleables that exit the map limits.
/// </summary>
[RequireComponent(typeof(EdgeCollider2D))]
public class PooleableLimit : MonoBehaviour
{
    /// <summary>
    /// <para>If the collider that exits the trigger has a Pooleable component then it deactivates it.</para>
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.activeSelf) return;

        var pooleable = other.GetComponent<Pooleable>();

        if (pooleable != null) pooleable.Deactivate();
        else Debug.LogError("A not pooleable went out of the limits! Name: " + other.name);
    }
}