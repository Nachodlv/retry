﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    [SerializeField] [Tooltip("Displayer of the score")]
    private ShowNumberUI scoreUI;

    [SerializeField] [Tooltip("Display of the current level score")]
    private ShowNumberUI levelScoreUI;
    
    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner enemySpawner;

    [SerializeField] [Tooltip("Spawner of player ships")]
    private PlayerSpawner playerSpawner;

    [SerializeField] [Tooltip("Manager of checkpoints")]
    private LevelManager levelManager;

    [SerializeField] [Tooltip("New Level text animator")]
    private NewLevelAnimator newLevelAnimator;
    
    private int score;
    private int levelScore;

    private void Awake()
    {
        enemySpawner.OnEnemyShipDestroyed += EnemyShipDestroyed;

        playerSpawner.OnAllShipsDestroyed += GameOver;
        playerSpawner.OnPlayerDestroyed += PlayerDestroyed;

        levelManager.LevelTransition += LevelTransition;
        levelManager.NextLevel += NewLevel;
    }

    private void Start()
    {
        scoreUI.UpdateValue(score);
        levelScoreUI.UpdateValue(levelScore);
    }

    /// <summary>
    /// <para>Sums the points with the level score increased by the current level</para>
    /// </summary>
    /// <param name="enemy">This parameter is ignored</param>
    /// <param name="points"></param>
    private void EnemyShipDestroyed(GameObject enemy, int points)
    {
        UpdateLevelScore(levelScore + points + (int) ((levelManager.currentLevel - 1) * 0.2f * points));
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
    /// <para>Sets the level score to zero</para>
    /// <para>Tells the EnemySpawner to repeat the previous spawns</para>
    /// <para>Tells the LevelManager to reset the level</para>
    /// <remarks>This method is executed when the player loses one live</remarks>
    /// </summary>
    private void PlayerDestroyed()
    {
        UpdateLevelScore(0);
        enemySpawner.RepeatSpawns();
        levelManager.ResetLevel();
    }

    /// <summary>
    /// <para>Sets the level score to newScore and updates the UI</para>
    /// </summary>
    /// <param name="newScore"></param>
    private void UpdateLevelScore(int newScore)
    {
        levelScore = newScore ;
        levelScoreUI.UpdateValue(levelScore);
    }

    /// <summary>
    /// <para>Calls the method NextLevel from the PlayerSpawner and the EnemySpawner</para>
    /// <para>Adds the level score to the score and sets the level score to zero</para>
    /// <remarks>This method is called when the transition time from the previous level is finished</remarks>
    /// </summary>
    /// <param name="level">The number of the new level</param>
    private void NewLevel(int level)
    {
        score += levelScore;
        UpdateLevelScore(0);
        scoreUI.UpdateValue(score);
        playerSpawner.NextLevel();
        enemySpawner.NextLevel(level);
    }

    /// <summary>
    /// <para>Stops the EnemySpawner from spawning enemies</para>
    /// <para>Pops up the new level text</para>
    /// <remarks>This method is called when the level time is over</remarks>
    /// </summary>
    private void LevelTransition()
    {
        enemySpawner.StopSpawning();
        newLevelAnimator.PopUp();
    }
}