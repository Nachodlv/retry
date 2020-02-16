using System;
using UnityEngine;

/// <summary>
/// <para>Moves the enemy continuously forward</para>
/// </summary>
public class EnemyController : Controller
{
    /// <summary>
    /// Continuously moves forward.
    /// </summary>
    private void FixedUpdate()
    {
        Move(new Vector2(-1, 0));
    }
}
