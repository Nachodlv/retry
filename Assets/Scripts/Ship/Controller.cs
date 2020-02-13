using System;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract event Action OnFireAbility;

    public event Action<Vector2> OnMove;

    public abstract void FireAbility();

    /// <summary>
    /// Invokes the OnMove event
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }
}
