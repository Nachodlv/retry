using System;
using UnityEngine;

/// <summary>
/// <para>Moves the enemy continuously forward</para>
/// </summary>
public class EnemyController : Controller
{
    public override event Action OnFireAbility;

    public override void FireAbility()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Continuously moves forward.
    /// </summary>
    private void FixedUpdate()
    {
        Move(new Vector2(-1, 0));
    }
}
