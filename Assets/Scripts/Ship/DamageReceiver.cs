using UnityEngine;

/// <summary>
/// <para>Reduces the health of the stats of this gameObject when it collides with another collider that also has
/// stats</para>
/// </summary>
[RequireComponent(typeof(Stats))]
public class DamageReceiver : MonoBehaviour
{
    private Stats _stats;
    private Pooleable _pooleable;

    /// <summary>
    /// <para>Adds a listener to the OnDie event</para>
    /// </summary>
    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _pooleable = GetComponent<Pooleable>();
        _stats.OnDie += OnDie;
    }

    /// <summary>
    /// <para>If the other collider has stats then the health of the this gameObject is reduced by the damage of the other
    /// collider.</para>
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        var colliderStats = other.gameObject.GetComponent<Stats>();
        if (colliderStats != null)
        {
            _stats.Health -= colliderStats.Damage;
        }
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
}