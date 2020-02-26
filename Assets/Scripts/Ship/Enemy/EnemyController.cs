using System;
using UnityEngine;

/// <summary>
/// <para>Moves the enemy continuously forward</para>
/// </summary>
public class EnemyController : Controller
{
    [SerializeField][Tooltip("How the enemy will move")] 
    private EnemyMovementEnum enemyMovementEnum = EnemyMovementEnum.Straight;

    private EnemyMovement _enemyMovement;
    private float initialY;
    enum EnemyMovementEnum
    {
        Straight,
        ZigZag
    }

    protected override void Awake()
    {
        base.Awake();
        AssignEnemyMovement();
    }

    private void OnEnable()
    {
        initialY = transform.position.y;
    }

    /// <summary>
    /// Gets the position to where it needs to move and calls the base method Move.
    /// </summary>
    private void FixedUpdate()
    {
        Move(_enemyMovement.Move(initialY));
    }

    /// <summary>
    /// <para>Initializes the _enemyMovement variable depending on the EnemyMovementEnum</para>
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void AssignEnemyMovement()
    {
        switch (enemyMovementEnum)
        {
            case EnemyMovementEnum.Straight:
                _enemyMovement = new StraightMovement();
                break;
            case EnemyMovementEnum.ZigZag:
                _enemyMovement = new ZigZagMovement();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
