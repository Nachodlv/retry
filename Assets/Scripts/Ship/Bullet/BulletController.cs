using System;

/// <summary>
/// <para>Controls the movement of a bullet</para>
/// </summary>
public class BulletController : Controller
{
    /// <summary>
    /// <para>Continuously moves forward.</para>
    /// </summary>
    private void FixedUpdate()
    {
        Move(transform.up);
    }
}
