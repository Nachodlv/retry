using System;
using UnityEngine;

/// <summary>
/// <para>Spawns an explosion where a ship is destroyed</para>
/// </summary>
public class ExplosionSpawner: MonoBehaviour
{
    [SerializeField][Tooltip("Spawner of player ships")]
    private PlayerSpawner playerSpawner;
    
    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner enemySpawner;

    [SerializeField] [Tooltip("Explosion prefab")]
    private Pooleable explosionPrefab;

    private ObjectPooler objectPooler;
    
    /// <summary>
    /// <para>Creates a pool of explosions and it subscribes to the EnemyShipDestroyed event from the EnemySpawner and
    /// to the OnShipDestroyed from the PlayerSpawner.</para>
    /// </summary>
    private void Awake()
    {
        objectPooler = new ObjectPooler();
        objectPooler.InstantiateObjects(5, explosionPrefab, "Explosions");
        enemySpawner.OnEnemyShipDestroyed += EnemyDestroyed;
        playerSpawner.OnShipDestroyed += ShipDestroyed;
    }

    /// <summary>
    /// <para>Calls the ShipDestroyed method with the enemy</para>
    /// <remarks>This method is called when an enemy ship is destroyed</remarks>
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="points">This parameter is ignored</param>
    private void EnemyDestroyed(GameObject enemy, int points)
    {
        ShipDestroyed(enemy);
    }

    /// <summary>
    /// <para>Gets the next explosion prefab from the pool and moves it to the position of the ship</para>
    /// </summary>
    /// <param name="ship">The ship that has been destroyed</param>
    private void ShipDestroyed(GameObject ship)
    {
        var explosion = objectPooler.GetNextObject();
        explosion.transform.position = ship.transform.position; 
    }
}
