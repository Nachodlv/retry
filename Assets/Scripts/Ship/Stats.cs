using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public event Action OnDie;
    
    [SerializeField] [Tooltip("Movement speed")]
    private float speed;

    [SerializeField] [Tooltip("Damage made when collision")]
    private float damage;

    [SerializeField] [Tooltip("Damage that can receive before destruction")]
    private float health;

    private float maxHealth;
    
    public float Speed => speed;
    public float Damage => damage;

    private void Awake()
    {
        maxHealth = health;
    }

    /// <summary>
    /// <para>If the health reaches zero then the OnDie event is invoked</para>
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            if(value <= 0) OnDie?.Invoke();
            health = value;
        }
    }

    /// <summary>
    /// <para>Sets the health equal to the max health</para>
    /// </summary>
    public void ResetStats()
    {
        health = maxHealth;
    }
}
