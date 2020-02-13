using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] [Tooltip("Displayer of the score")]
    private ScoreUI _scoreUi;

    [SerializeField] [Tooltip("Spawner of enemies")]
    private EnemySpawner _enemySpawner;

    private float score;

    private void Awake()
    {
        _scoreUi.UpdateScore(score);
        _enemySpawner.OnPointsScored += PointsScored;
    }

    private void PointsScored(float points)
    {
        score += points;
        _scoreUi.UpdateScore(score);
    }
}