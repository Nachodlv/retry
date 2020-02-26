using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// <para>Moves the enemy in Sin way</para>
/// </summary>
public class ZigZagMovement : EnemyMovement
{
    private readonly float maxY;
    private readonly float minY;
    private bool goingUp;
    private readonly Transform transform;

    public ZigZagMovement(Transform transform, float shipWidth)
    {
        var cameraBounds = CameraBounds.GetCameraBounds(Camera.main);
        maxY = cameraBounds.max.y - shipWidth/2;
        minY = cameraBounds.min.y + shipWidth/2;
        this.transform = transform;
    }

    /// <summary>
    /// <para>Moves up or down until it reaches the camera bounds and then it goes to the
    /// opposite side. It also goes to the left</para>
    /// </summary>
    /// <returns></returns>
    public Vector2 Move()
    {
        var y = transform.position.y;
        if (goingUp)
        {
            if (y > maxY) goingUp = false;
            return new Vector2(-0.5f, 1);
        }

        if (y < minY) goingUp = true;
        return new Vector2(-0.5f, -1);
    }

    /// <summary>
    /// <para>Changes the bool goingUp to true or false in a random way.</para>
    /// <remarks>This method is called when the enemy ship is spawn in the scene.</remarks>
    /// </summary>
    public void Spawn()
    {
        goingUp = Random.Range(0f, 1f) > 0.5f;
    }
}