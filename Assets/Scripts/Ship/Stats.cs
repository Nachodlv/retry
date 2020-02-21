using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public event Action OnDie;

    [SerializeField] [Tooltip("Movement speed")]
    private float speed;

    public float Speed => speed;

    [SerializeField] [Tooltip("Damage made when collision")]
    private float damage;

    public float Damage => damage;

    [SerializeField] [Tooltip("Damage that can receive before destruction")]
    private float health;

    [SerializeField][Tooltip("Speed limit")]
    private float maxSpeed;
    
    private float maxHealth;
    private float baseDamage;
    private float baseSpeed;
    private float baseMaxHealth;
    
    /// <summary>
    /// <para>If the health reaches zero then the OnDie event is invoked</para>
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            if (value <= 0) OnDie?.Invoke();
            health = value;
        }
    }

    private void Awake()
    {
        maxHealth = health;
        baseDamage = damage;
        baseSpeed = speed;
        baseMaxHealth = maxHealth;
    }
    
    /// <summary>
    /// <para>Sets the health equal to the max health</para>
    /// </summary>
    public void ResetStats()
    {
        health = maxHealth;
    }

    /// <summary>
    /// <para>Increases the stats depending on the current level</para>
    /// </summary>
    /// <param name="level"></param>
    public void IncreaseStats(int level)
    {
        maxHealth = baseMaxHealth * level * 0.1f;
        speed = Math.Min(baseSpeed * level * 0.1f, maxSpeed);
        damage = baseDamage * level * 0.5f;
    }
}