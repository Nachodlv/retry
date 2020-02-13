using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[Serializable]
class EnemySpawn
{
    [SerializeField] [Tooltip("Enemy prefab to be spawned")]
    private Pooleable _enemy;

    [SerializeField] [Tooltip("Points given to the player when the enemy is defeated")]
    private float _points;

    [SerializeField]
    [Tooltip("Probability to be spawned in each tick. The sum of all the spawn probabilities shouldn't surpass 1")]
    [Range(0, 1)]
    private float _spawnProbability;

    private ObjectPooler _objectPooler;

    public float SpawnProbability => _spawnProbability;
    public Pooleable Enemy => _enemy;

    public float Points => _points;

    public void InitializePool(Action<List<Pooleable>> newEnemies)
    {
        _objectPooler = new ObjectPooler();
        _objectPooler.InstantiateObjects(10, _enemy, _enemy.name, newEnemies);
    }

    public Pooleable GetNextEnemy()
    {
        return _objectPooler.GetNextObject();
    }
}

/// <summary>
/// <para>Spawns enemies every tick</para>
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public event Action<float> OnPointsScored;

    [SerializeField] [Tooltip("Time between each enemy spawn")]
    private float tickDuration;

    [SerializeField] [Tooltip("Stats modifier of the enemies")]
    private int _dificultyLevel;

    [SerializeField] [Tooltip("Enemies to be spawned")]
    private EnemySpawn[] _enemies;

    [Header("Spawn range")] [SerializeField] [Tooltip("Limits in the Y Axis")]
    private Vector2 limitY;

    [SerializeField] [Tooltip("Limit in the X Axis")]
    private float limitX;

    private float currentTick;

    /// <summary>
    /// <para>Initializes the enemies pools</para>
    /// </summary>
    private void Awake()
    {
        foreach (var enemySpawn in _enemies)
        {
            enemySpawn.InitializePool((enemies) => NewEnemies(enemies, enemySpawn.Points));
        }
    }

    private void NewEnemies(List<Pooleable> enemies, float score)
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Stats>().OnDie += () => OnPointsScored?.Invoke(score);
        }
    }

    /// <summary>
    /// <para>If the current tick equals or greater than the tick duration then it spawns an enemy</para>
    /// </summary>
    private void Update()
    {
        if (currentTick < tickDuration)
        {
            currentTick += Time.deltaTime;
            return;
        }

        currentTick = 0;
        var randomNumber = Random.Range(0f, 1f);
        var randomHeight = Random.Range(limitY.x, limitY.y);
        var enemy = GetRandomEnemy(randomNumber).GetNextEnemy();
        enemy.transform.position = new Vector2(limitX, randomHeight);
    }

    /// <summary>
    /// <para>Given a random number between 0 and 1 returns an EnemySpawn</para>
    /// </summary>
    /// <param name="randomNumber"></param>
    /// <returns>The random EnemySpawn. If _enemies is empty then the return will be null</returns>
    private EnemySpawn GetRandomEnemy(float randomNumber)
    {
        if (_enemies.Length == 0) return null;
        
        var probabilitySum = 0f;
        foreach (var enemySpawn in _enemies)
        {
            if (enemySpawn.SpawnProbability + probabilitySum >= randomNumber) return enemySpawn;
            probabilitySum += enemySpawn.SpawnProbability;
        }

        return _enemies[0];
    }
}