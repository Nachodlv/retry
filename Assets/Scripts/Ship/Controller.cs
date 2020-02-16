using System;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public abstract class Controller : MonoBehaviour
{
    private Controllable controllable;

    protected void Awake()
    {
        controllable = GetComponent<Controllable>();
    }

    /// <summary>
    /// Invokes the OnMove event
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        controllable.Move(direction);
    }

    /// <summary>
    /// <para>Invokes the OnShoot event.</para>
    /// </summary>
    public void Shoot()
    {
        controllable.Shoot();
    }
}
