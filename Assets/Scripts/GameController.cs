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

    private float score;

    private void Awake()
    {
        scoreUi.UpdateScore(score);
        enemySpawner.OnPointsScored += PointsScored;
    }

    private void PointsScored(float points)
    {
        score += points;
        scoreUi.UpdateScore(score);
    }
}