using UnityEngine;

/// <summary>
/// <para>Moves the RigidBody in the direction specified by the Controller</para>
/// </summary>
[RequireComponent(typeof(Stats), typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] [Tooltip("Controller that will tell the mover when to move")]
    private Controller controller;

    private Stats _stats;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        controller.OnMove += Move;
    }

    /// <summary>
    /// <para>Sets the move direction to the direction passed as parameter</para>
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector2 direction)
    {
        _moveDirection = direction;
    }
    
    /// <summary>
    /// <para>Adds force to the RigidBody2D in the direction specified by _moveDirection</para>
    /// </summary>
    private void Update()
    {
        _rigidbody2D.AddForce(_moveDirection * _stats.Speed);
    }
}