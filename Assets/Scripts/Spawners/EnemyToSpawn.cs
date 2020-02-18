using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Used to input the enemies with their respective points and spawn probabilities via the Unity Editor</para>
/// </summary>
[Serializable]
public class EnemyToSpawn
{
    [SerializeField] [Tooltip("Enemy prefab to be spawned")]
    private Pooleable enemy;

    [SerializeField] [Tooltip("Points given to the player when the enemy is defeated")]
    private float points;

    [SerializeField]
    [Tooltip("Probability to be spawned in each tick. The sum of all the spawn probabilities shouldn't surpass 1")]
    [Range(0, 1)]
    private float spawnProbability;

    private ObjectPooler _objectPooler;

    public float SpawnProbability => spawnProbability;
    public Pooleable Enemy => enemy;

    public float Points => points;

    /// <summary>
    /// <para>Creates a pool of enemies for the EnemySpawn</para>
    /// </summary>
    /// <param name="newEnemies">Action that will be executed when the pool of enemies grows</param>
    public void InitializePool(Action<List<Pooleable>> newEnemies)
    {
        _objectPooler = new ObjectPooler();
        _objectPooler.InstantiateObjects((int) spawnProbability * 10 + 3, enemy, enemy.name, newEnemies);
    }

    /// <summary>
    /// <para>Returns the next enemy from the pool</para>
    /// </summary>
    /// <returns></returns>
    public Pooleable GetNextEnemy()
    {
        return _objectPooler.GetNextObject();
    }
}