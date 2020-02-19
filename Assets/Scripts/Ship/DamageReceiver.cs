using System;
using UnityEngine;

/// <summary>
/// <para>Reduces the health of the stats of this gameObject when it collides with another collider that also has
/// stats</para>
/// </summary>
[RequireComponent(typeof(Stats), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of force that will be applied to the RigidBody when it collides with another " +
             "RigidBody")]
    private float bounceForce;

    [SerializeField] [Tooltip("Time in seconds where the ships cannot be hit again")]
    private float invincibleTime;

    [SerializeField] [Tooltip("If the ship should blink when hit")]
    private bool blink;

    private Stats _stats;
    private Pooleable _pooleable;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _currentInvincibleTime;
    private bool _invincible;
    private Color _originalColor;

    /// <summary>
    /// <para>Adds a listener to the OnDie event</para>
    /// </summary>
    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pooleable = GetComponent<Pooleable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _stats.OnDie += OnDie;
    }

    /// <summary>
    /// <para>If the other collider has stats then the health of this gameObject is reduced by the damage of the other
    /// collider. This GameObject becomes invincible for few seconds </para>
    /// <para>If the GameObject is currently invincible then the collision is ignored</para>
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_invincible) return;

        var colliderStats = other.gameObject.GetComponent<Stats>();
        if (colliderStats == null) return;

        _stats.Health -= colliderStats.Damage;
        _rigidbody2D.AddForce((other.transform.position - transform.position) * bounceForce);

        _invincible = true;
    }

    /// <summary>
    /// <para>If this GameObject has a pooler then calls the method Deactivated form it.
    /// If this GameObject does not have a pooler then it deactivated the GameObject</para>
    /// </summary>
    private void OnDie()
    {
        if (_pooleable != null)
        {
            _pooleable.Deactivate();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// <para>Reduces the duration of the invincible time.</para>
    /// <para>The sprite blinks while the GameObject in invincible</para>
    /// </summary>
    private void Update()
    {
        if (!_invincible) return;

        if (_currentInvincibleTime >= invincibleTime)
        {
            _currentInvincibleTime = 0;
            _invincible = false;
            if (blink) _spriteRenderer.color = _originalColor;
            return;
        }

        _currentInvincibleTime += Time.deltaTime;

        if (!blink) return;

        var color = _originalColor;
        var lowAlphaColor = color;
        lowAlphaColor.a /= 2;
        _spriteRenderer.color = Color.Lerp(color, lowAlphaColor, Mathf.PingPong(Time.time * 2, 1));
    }
}