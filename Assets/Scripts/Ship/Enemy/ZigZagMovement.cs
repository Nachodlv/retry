using System;
using UnityEngine;

/// <summary>
/// <para>Moves the enemy in Sin way</para>
/// </summary>
public class ZigZagMovement: EnemyMovement
{
    private float maxY;
    private float minY;
    private float initialY;
    
    public ZigZagMovement() 
    {
        var cameraBounds = CameraBounds.GetCameraBounds(Camera.main);
        maxY = cameraBounds.max.y;
        minY = cameraBounds.min.y;
        
    }
    
    public Vector2 Move(float initialY)
    {
        var phase = ((initialY - minY) / (maxY - minY))
                    * (Mathf.PI*2 - 0) + 0;
        var middle = (maxY - minY) / 2 + minY;
        return new Vector2(-1, (Mathf.Sin(Time.time + phase) + 1f) / 2f);
    }
    
}
