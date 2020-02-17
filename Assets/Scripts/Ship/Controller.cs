using UnityEngine;

/// <summary>
/// <para>Uses a Controllable to control a GameObject</para>
/// </summary>
[RequireComponent(typeof(Controllable))]
public abstract class Controller : MonoBehaviour
{
    protected Controllable controllable;

    protected virtual void Awake()
    {
        controllable = GetComponent<Controllable>();
    }

    /// <summary>
    /// <para>Calls the Move method from the controllable</para>
    /// </summary>
    /// <param name="direction"></param>
    protected void Move(Vector2 direction)
    {
        controllable.Move(direction);
    }

    /// <summary>
    /// <para>Calls the MoveToPosition method from the controllable</para>
    /// </summary>
    /// <param name="position"></param>
    protected void MoveToPosition(Vector2 position)
    {
        controllable.MoveToPosition(position);
    }

    /// <summary>
    /// <para>Calls the Shoot method from the controllable</para>
    /// </summary>
    protected void Shoot()
    {
        controllable.Shoot();
    }
}
