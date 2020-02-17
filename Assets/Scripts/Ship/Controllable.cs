using System;
using UnityEngine;

/// <summary>
/// <para>A Controllable is used so the GameObject can be controlled by a controller</para>
/// </summary>
[RequireComponent(typeof(Controller))]
public class Controllable: MonoBehaviour
{
    public event Action OnFireAbility;
    public event Action<Vector2> OnMove;
    public event Action OnShoot;

    public event Action<Vector2> OnMovePosition;

    /// <summary>
    /// <para>Invokes the OnMove event</para>
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }
    
    /// <summary>
    /// <para>Invokes the OnShoot event</para>
    /// </summary>
    public void Shoot()
    {
        OnShoot?.Invoke();
    }
    
    /// <summary>
    /// <para>Invokes the OnMovePosition event</para>
    /// </summary>
    /// <param name="position"></param>
    public void MoveToPosition(Vector2 position)
    {
        OnMovePosition?.Invoke(position);
    }

    /// <summary>
    /// <para>Invokes the OnFireAbility event</para>
    /// </summary>
    public void FireAbility()
    {
        OnFireAbility?.Invoke();
    }
}
