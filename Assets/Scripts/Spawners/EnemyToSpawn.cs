using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public float Points => points;
    public List<Stats> enemyStats;

    /// <summary>
    /// <para>Creates a pool of enemies for the EnemySpawn</para>
    /// </summary>
    /// <param name="newEnemies">Action that will be executed when the pool of enemies grows</param>
    public void InitializePool(Action<List<Pooleable>> newEnemies)
    {
        var quantityOfEnemies = (int) spawnProbability * 10 + 3;
        _objectPooler = new ObjectPooler();
        enemyStats = new List<Stats>(quantityOfEnemies);
        _objectPooler.InstantiateObjects(quantityOfEnemies, enemy, enemy.name, enemies =>
        {
            GetEnemyStats(enemies);
            newEnemies(enemies);
        });
    }

    /// <summary>
    /// <para>Returns the next enemy from the pool</para>
    /// </summary>
    /// <returns></returns>
    public Pooleable GetNextEnemy()
    {
        return _objectPooler.GetNextObject();
    }

    /// <summary>
    /// <para>Deactivates the enemy pool</para>
    /// </summary>
    public void DeactivateEnemy()
    {
        _objectPooler.DeactivatePooleables();
    }

    /// <summary>
    /// <para>Increases the stats of the current enemies inside the pool</para>
    /// </summary>
    /// <param name="level"></param>
    public void UpdateStats(int level)
    {
        foreach (var enemyStat in enemyStats)
        {
            enemyStat.IncreaseStats(level);
        }
    }

    /// <summary>
    /// <para>Gets the stats of the new enemies added to the pool</para>
    /// </summary>
    /// <param name="enemies"></param>
    private void GetEnemyStats(List<Pooleable> enemies)
    {
        foreach (var pooleable in enemies)
        {
            enemyStats.Add(pooleable.GetComponent<Stats>());
        }
    }
}