
using UnityEngine;

/// <summary>
/// <para>Defines the enemy movement</para>
/// <remarks>The class is used by the EnemyController</remarks>
/// </summary>
public interface EnemyMovement
{
    /// <summary>
    /// <para>Returns the position to where the enemy should move</para>
    /// </summary>
    /// <param name="initialY">The position in the Y Axis where the enemy has spawned</param>
    /// <returns></returns>
    Vector2 Move(float initialY);
}
