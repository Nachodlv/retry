
using UnityEngine;

/// <summary>
/// <para>Moves the enemy in a straight line by decreasing the Y Axis</para>
/// </summary>
public class StraightMovement : EnemyMovement
{

    /// <summary>
    /// <para>Returns a vector pointing to the left</para>
    /// </summary>
    /// <param name="initialY"></param>
    /// <returns></returns>
    public Vector2 Move(float initialY)
    {
        return new Vector2(-1, 0);
    }
}