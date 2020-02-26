
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
    /// <returns></returns>
    Vector2 Move();
    
    /// <summary>
    /// <para>Tells the EnemyMovement that the enemy ship has spawn again</para>
    /// </summary>
    void Spawn();
}
