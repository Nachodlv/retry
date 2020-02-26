using System;
using UnityEngine;

/// <summary>
/// <para>Activates the explosion animation</para>
/// </summary>
public class ExplosionController: Pooleable
{
    [SerializeField] [Tooltip("Animator")]
    private Animator animator;

    private static readonly int Explosion = Animator.StringToHash("explode");

    public override void Activate()
    {
        base.Activate();
        animator.SetTrigger(Explosion);
    }
}
