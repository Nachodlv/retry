using UnityEngine;

/// <summary>
/// <para>Moves the RigidBody in the direction specified by the Controller</para>
/// </summary>
[RequireComponent(typeof(Stats), typeof(Rigidbody2D), typeof(Controllable))]
public class Mover : MonoBehaviour
{
    [SerializeField] [Tooltip("If constant velocity is true then the script will set the velocity to the RigidBody")]
    private bool constantVelocity;

    private Stats _stats;
    private Vector2 _moveDirection;
    private Vector2 _moveToPosition;
    private bool _isMovingByPosition;
    private Rigidbody2D _rigidbody2D;
    private Controllable controllable;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        controllable = GetComponent<Controllable>();
        controllable.OnMove += Move;
        controllable.OnMovePosition += MoveToPosition;
    }

    /// <summary>
    /// <para>Sets the move direction to the direction passed as parameter</para>
    /// </summary>
    /// <param name="direction"></param>
    private void Move(Vector2 direction)
    {
        _isMovingByPosition = false;
        _moveDirection = direction;
    }

    /// <summary>
    /// <para>Sets the _moveToPosition to the position passed in the parameters</para>
    /// </summary>
    /// <param name="position"></param>
    private void MoveToPosition(Vector2 position)
    {
        _isMovingByPosition = true;
        _moveToPosition = position;
    }

    /// <summary>
    /// <para>If constantVelocity is false, adds force to the RigidBody2D in the direction specified by _moveDirection</para>
    /// <para>If constantVelocity is true, sets the velocity equals to the _moveDirection</para>
    /// <para>If _isMovingByPosition is true, moves the position towards the _movePosition</para>
    /// </summary>
    private void Update()
    {
        if (constantVelocity)
        {
            _rigidbody2D.velocity = _moveDirection * _stats.Speed;
            return;
        }

        if (_isMovingByPosition)
        {
            Vector2 position = transform.position;
            transform.position =
                Vector2.Lerp(position, _moveToPosition,
                    Time.deltaTime * _stats.Speed / 10); // TODO move this to a variable
            return;
        }

        _rigidbody2D.AddForce(_moveDirection * _stats.Speed);
    }
}