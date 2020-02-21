using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    [SerializeField] [Tooltip("Displayer of the score")]
    private ShowNumberUI scoreUI;
    
    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner enemySpawner;

    [SerializeField] [Tooltip("Spawner of player ships")]
    private PlayerSpawner playerSpawner;

    [SerializeField] [Tooltip("Manager of checkpoints")]
    private LevelManager levelManager;
    
    private float score;

    private void Awake()
    {
        scoreUI.UpdateValue(score);
        enemySpawner.OnPointsScored += PointsScored;

        playerSpawner.OnAllShipsDestroyed += GameOver;
        playerSpawner.OnShipDestroyed += ShipDestroyed;

        levelManager.LevelTransition += LevelTransition;
        levelManager.NextLevel += NewLevel;
    }

    /// <summary>
    /// <para>Sums the points with the score increased by the current level</para>
    /// </summary>
    /// <param name="points"></param>
    private void PointsScored(float points)
    {
        UpdateScore(score + points + (int) ((levelManager.currentLevel - 1) * 0.2f * points));
    }

    /// <summary>
    /// <para>Resets the Scene</para>
    /// <remarks>This method is executed when the lives of the player reaches zero</remarks>
    /// </summary>
    private void GameOver()
    {
        SceneManager.LoadScene(0);
        Debug.Log("GG");
    }

    /// <summary>
    /// <para>Sets the score to zero</para>
    /// <para>Tells the EnemySpawner to repeat the previous spawns</para>
    /// <para>Tells the LevelManager to reset the level</para>
    /// <remarks>This method is executed when the player loses one live</remarks>
    /// </summary>
    private void ShipDestroyed()
    {
        UpdateScore(0);
        enemySpawner.RepeatSpawns();
        levelManager.ResetLevel();
    }

    /// <summary>
    /// <para>Sets the score to newScore and updates the UI</para>
    /// </summary>
    /// <param name="newScore"></param>
    private void UpdateScore(float newScore)
    {
        score = newScore ;
        scoreUI.UpdateValue(score);
    }

    /// <summary>
    /// <para>Calls the method NextLevel from the PlayerSpawner and the EnemySpawner</para>
    /// <remarks>This method is called when the transition time from the previous level is finished</remarks>
    /// </summary>
    /// <param name="level">The number of the new level</param>
    private void NewLevel(int level)
    {
        playerSpawner.NextLevel();
        enemySpawner.NextLevel(level);
    }

    /// <summary>
    /// <para>Stops the EnemySpawner from spawning enemies</para>
    /// <remarks>This method is called when the level time is over</remarks>
    /// </summary>
    private void LevelTransition()
    {
        enemySpawner.StopSpawning();
    }
}