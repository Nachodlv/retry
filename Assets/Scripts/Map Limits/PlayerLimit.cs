using UnityEngine;

/// <summary>
/// <para>Limits the movements of the player inside the visible area</para>
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class PlayerLimit : MonoBehaviour
{
    private Collider2D myCollider;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Ignores collisions if the GameObject that collides does not have the tag "Player"
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.collider, myCollider);
        }
    }
}
