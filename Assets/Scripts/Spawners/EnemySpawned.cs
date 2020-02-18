using UnityEngine;

/// <summary>
/// <para>Used to store the enemies that the EnemySpawner has already spawned</para>
/// </summary>
public struct EnemySpawned
{
    private EnemyToSpawn enemy;
    private Vector2 position;
    private float time;

    public EnemyToSpawn Enemy => enemy;
    public Vector2 Position => position;
    public float Time => time;

    public EnemySpawned(EnemyToSpawn enemy, Vector2 position, float time)
    {
        this.enemy = enemy;
        this.position = position;
        this.time = time;
    }
}
