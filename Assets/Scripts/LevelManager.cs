using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// <para>Manages the levels transitions</para>
/// </summary>
public class LevelManager : MonoBehaviour
{
    public event Action<int> NextLevel;
    public event Action LevelTransition;
    public float LevelDuration => levelDuration;

    [SerializeField] [Tooltip("Duration of each level in seconds")]
    private float levelDuration;

    [SerializeField] [FormerlySerializedAs("initialLevel")] [Tooltip("Starting level")]
    private int currentLevel;

    [SerializeField] [Tooltip("Level UI")] private ShowNumberUI levelUI;

    [SerializeField] [Tooltip("Wait time between level transitions")]
    private float levelTransitionTime;

    private float currentLevelDuration;
    private bool transitioning;

    /// <summary>
    /// <para>Resets the timer of the current level</para>
    /// </summary>
    public void ResetLevel()
    {
        currentLevelDuration = 0;
        transitioning = false;
    }

    private void Awake()
    {
        levelUI.UpdateValue(currentLevel);
    }

    /// <summary>
    /// <para>If currentLevelDuration surpass the levelDuration then the transition will start and it will invoke the
    /// LevelTransition event</para>
    /// <para>If transitioning is true and the currentLevelDuration is higher than the levelTransitionTime then the
    /// NextLevel event is Invoke and the currentLevel is increase.</para>
    /// </summary>
    private void Update()
    {
        if (transitioning && currentLevelDuration >= levelTransitionTime)
        {
            currentLevel++;
            levelUI.UpdateValue(currentLevel);
            NextLevel?.Invoke(currentLevel);
            currentLevelDuration = 0;
            transitioning = false;
            return;
        }
        if (currentLevelDuration >= levelDuration)
        {
            transitioning = true;
            LevelTransition?.Invoke();
            currentLevelDuration = 0;
            return;
        }

        currentLevelDuration += Time.deltaTime;
    }
}