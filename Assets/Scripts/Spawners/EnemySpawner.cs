using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private EnemyToSpawn[] _enemies;

    [SerializeField] [Tooltip("Offset in the X axis that the enemies will be spawned")]
    private float xOffset;

    [SerializeField] [Tooltip("Used to get the checkpoint time")]
    private CheckPointerController checkPointerController;

    private float minY;
    private float maxY;
    private float limitX;
    private float currentTick;
    private EnemySpawned[] spawns;
    private int currentSpawnIndex;
    private int totalSpawns;
    private float startingTime;
    private int coroutinesInProgress;

    /// <summary>
    /// <para>Starts spawning enemies</para>
    /// </summary>
    public void BeginSpawning()
    {
        totalSpawns = 0;
        RepeatSpawns();
    }

    /// <summary>
    /// <para>Starts spawning enemies using the enemies that had previously spawn. If there is no more records the
    /// it will start spawning new enemies</para>
    /// </summary>
    public void RepeatSpawns()
    {
        currentSpawnIndex = 0;
        currentTick = 0;
        startingTime = Time.time;
        RemoveCurrentShips();
    }

    /// <summary>
    /// <para>Initializes the enemies pools and the spawns array</para>
    /// <para>Calls the BeginSpawning method</para>
    /// </summary>
    private void Awake()
    {
        spawns = new EnemySpawned[(int) (checkPointerController.checkPointTime / tickDuration)];
        foreach (var enemySpawn in _enemies)
        {
            enemySpawn.InitializePool((newEnemies) => NewEnemies(newEnemies, enemySpawn.Points));
        }

        BeginSpawning();
    }

    private void Start()
    {
        SetUpLimits();
    }

    /// <summary>
    /// <para>Set up the positions for the enemy spawning taking into account the camera bounds.</para>
    /// </summary>
    private void SetUpLimits()
    {
        var camera = Camera.main;
        if (camera == null) return;
        var bounds = CameraBounds.GetCameraBounds(camera);
        var max = bounds.max;
        var min = bounds.min;
        limitX = max.x + xOffset;
        minY = min.y;
        maxY = max.y;
    }

    /// <summary>
    /// <para>Subscribes to the OnDie event of the newEnemies</para>
    /// <remarks>This method is called when the enemy pool grows</remarks>
    /// </summary>
    /// <param name="newEnemies"></param>
    /// <param name="score">The points given when the any of the enemies from the newEnemies list is destroyed</param>
    private void NewEnemies(List<Pooleable> newEnemies, float score)
    {
        foreach (var enemy in newEnemies)
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
        SpawnEnemy();
    }

    /// <summary>
    /// <para>Given a random number between 0 and 1 returns an EnemySpawn</para>
    /// </summary>
    /// <param name="randomNumber"></param>
    /// <returns>The random EnemySpawn. If _enemies is empty then the return will be null</returns>
    private EnemyToSpawn GetRandomEnemy(float randomNumber)
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

    /// <summary>
    /// <para>It spawns an enemy</para>
    /// <para>If there are no more spawn records then it will spawn a new enemy. If there are still spawn records,
    /// it will spawn the enemy specified in the record</para>
    /// </summary>
    private void SpawnEnemy()
    {
        if (currentSpawnIndex >= totalSpawns)
        {
            if(coroutinesInProgress == 0) SpawnNewEnemy();
        }
        else
        {        
            StartCoroutine(SpawnRecordEnemy(currentSpawnIndex));
            currentSpawnIndex++;
        }
    }

    /// <summary>
    /// <para>Spawns a random enemy in a random position and saves it to the spawn array</para>
    /// </summary>
    private void SpawnNewEnemy()
    {
        var randomNumber = Random.Range(0f, 1f);
        var randomHeight = Random.Range(minY, maxY);
        var enemy = GetRandomEnemy(randomNumber);
        var pooleable = enemy.GetNextEnemy();
        var position = new Vector2(limitX, randomHeight);
        pooleable.transform.position = position;
        spawns[currentSpawnIndex] = new EnemySpawned(enemy, position, Time.time - startingTime);
        currentSpawnIndex++;
        totalSpawns++;
    }

    /// <summary>
    /// <para>Spawns the enemy stored in spawns at the index passed as parameter</para>
    /// <para>Waits till the time of spawn</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private IEnumerator SpawnRecordEnemy(int index)
    {
        coroutinesInProgress++;
        var spawn = spawns[index];
        while (Time.time- startingTime < spawn.Time)
            yield return null;
        
        var enemySpawned = spawn.Enemy.GetNextEnemy();
        enemySpawned.transform.position = spawn.Position;
        coroutinesInProgress--;
        yield return null;
    }

    /// <summary>
    /// <para>Removes the ships present in the Scene</para>
    /// </summary>
    private void RemoveCurrentShips()
    {
        foreach (var enemyToSpawn in _enemies)
        {
            enemyToSpawn.DeactivateEnemy();
        }
    }
}