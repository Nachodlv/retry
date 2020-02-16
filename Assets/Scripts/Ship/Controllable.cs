using System;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Controllable: MonoBehaviour
{
    public event Action OnFireAbility;
    public event Action<Vector2> OnMove;
    public event Action OnShoot;

    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }

    public void Shoot()
    {
        OnShoot?.Invoke();
    }

    public void FireAbility()
    {
        OnFireAbility?.Invoke();
    }
}
