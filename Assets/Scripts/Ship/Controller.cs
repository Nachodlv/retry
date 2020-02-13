using System;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract event Action OnFireAbility;

    public event Action<Vector2> OnMove;
    public event Action OnShoot;

    public abstract void FireAbility();

    /// <summary>
    /// Invokes the OnMove event
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    /// <summary>
    /// <para>Invokes the OnShoot event.</para>
    /// </summary>
    public void Shoot()
    {
        OnShoot?.Invoke();
    }
}
