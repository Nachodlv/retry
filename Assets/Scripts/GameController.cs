using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [Tooltip("Displayer of the score")]
    private ShowNumberUI scoreUI;
    
    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner enemySpawner;

    [SerializeField] [Tooltip("Spawner of player ships")]
    private PlayerSpawner playerSpawner;

    private float score;

    private void Awake()
    {
        scoreUI.UpdateValue(score);
        enemySpawner.OnPointsScored += PointsScored;

        playerSpawner.OnAllShipsDestroyed += GameOver;
        playerSpawner.OnShipDestroyed += ShipDestroyed;
    }

    /// <summary>
    /// <para>Sums the points with the score</para>
    /// </summary>
    /// <param name="points"></param>
    private void PointsScored(float points)
    {
        UpdateScore(score + points);
    }

    /// <summary>
    /// <para>Ends the game</para>
    /// <remarks>This method is executed when the lives of the player reaches zero</remarks>
    /// </summary>
    private void GameOver()
    {
        Debug.Log("GG");
    }

    /// <summary>
    /// <para>Sets the score to zero</para>
    /// <remarks>This method is executed when the player loses one live</remarks>
    /// </summary>
    private void ShipDestroyed()
    {
        UpdateScore(0);
    }

    /// <summary>
    /// <para>Sets the score to newScore and updates the UI</para>
    /// </summary>
    /// <param name="newScore"></param>
    private void UpdateScore(float newScore)
    {
        score = newScore;
        scoreUI.UpdateValue(score);
    }
}