using System;

/// <summary>
/// <para>Controls the movement of a bullet</para>
/// </summary>
public class BulletController : Controller
{
    public override event Action OnFireAbility;
    public override void FireAbility()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// <para>Continuously moves forward.</para>
    /// </summary>
    private void FixedUpdate()
    {
        Move(transform.up);
    }
}
