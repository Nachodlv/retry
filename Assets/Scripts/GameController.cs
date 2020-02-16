using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [Tooltip("Displayer of the score")]
    private ScoreUI scoreUi;

    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner enemySpawner;

    [SerializeField][Tooltip("Quantity of lives the player has")]
    private int lives = 2;

    [SerializeField][Tooltip("Player prefab")]
    private PlayerController player;
    [SerializeField] [Tooltip("Touch controller to move the player ship")]
    private SimpleTouchController touchController;

    private float score;
    private PlayerSpawner playerSpawner;

    private void Awake()
    {
        scoreUi.UpdateScore(score);
        enemySpawner.OnPointsScored += PointsScored;
        
        playerSpawner = new PlayerSpawner(lives, player, touchController);
        playerSpawner.InstantiatePlayerShips();
        playerSpawner.OnAllShipsDestroyed += GameOver;
    }

    private void PointsScored(float points)
    {
        score += points;
        scoreUi.UpdateScore(score);
    }

    private void GameOver()
    {
        Debug.Log("GG");
    }
}