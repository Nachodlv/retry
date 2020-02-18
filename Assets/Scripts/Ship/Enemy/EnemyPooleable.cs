using System;
using UnityEngine;

/// <summary>
/// <para>Pooleable that resets its stats when activated and removes its bullets when deactivated</para>
/// </summary>
[RequireComponent(typeof(Stats), typeof(Shooter))]
public class EnemyPooleable : Pooleable
{
    private Shooter shooter;
    private Stats stats;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        shooter = GetComponent<Shooter>();
    }

    /// <summary>
    /// <para>Resets its stats and calls the base method</para>
    /// </summary>
    public override void Activate()
    {
        stats.ResetStats();
        base.Activate();
    }

    /// <summary>
    /// <para>Removes its bullets and calls the base method</para>
    /// </summary>
    public override void Deactivate()
    {
        shooter.RemoveBullets();
        base.Deactivate();
    }
}