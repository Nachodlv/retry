using UnityEngine;

/// <summary>
/// <para>Reduces the health of the stats of this gameObject when it collides with another collider that also has
/// stats</para>
/// </summary>
[RequireComponent(typeof(Stats))]
public class DamageReceiver : MonoBehaviour
{
    private Stats _stats;

    /// <summary>
    /// <para>Adds a listener to the OnDie event. When the health reaches zero the gameObject is deactivated</para>
    /// </summary>
    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _stats.OnDie += () => { gameObject.SetActive(false); };
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
}