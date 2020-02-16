
using System;
using UnityEngine;

/// <summary>
/// <para>Pooleable that resets its stats when activated</para>
/// </summary>
[RequireComponent(typeof(Stats))]
 public class PooleableWithStats: Pooleable
 {

     private Stats stats;

     private void Awake()
     {
         stats = GetComponent<Stats>();
     }

     /// <summary>
     /// <para>Resets its stats and calls the base method</para>
     /// </summary>
     public override void Activate(ObjectPooler pooleable)
     {
         stats.ResetStats();
         base.Activate(pooleable);
     }
 }
