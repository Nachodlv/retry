
using UnityEngine;

/// <summary>
/// <para>Moves the enemy in a straight line by decreasing the X Axis</para>
/// </summary>
public class StraightMovement : EnemyMovement
{

    /// <summary>
    /// <para>Returns a vector pointing to the left</para>
    /// </summary>
    /// <returns></returns>
    public Vector2 Move()
    {
        return new Vector2(-1, 0);
    }

    /// <summary>
    /// <para>Does nothing</para>
    /// </summary>
    public void Spawn()
    {
    }
}