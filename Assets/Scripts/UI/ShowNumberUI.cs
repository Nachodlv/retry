using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using Utils;

/// <summary>
/// <para>Displays a float in a TextMeshProUGUI</para>
/// </summary>
public class ShowNumberUI : MonoBehaviour
{
    [SerializeField][Tooltip("Text where the value will appear")]
    private TextMeshProUGUI valueText;

    [SerializeField] [Tooltip("Time it takes to change to a new value")]
    private float timeToChangeValue = 1;

    [SerializeField] [Tooltip("Minimum time it takes to change a number")]
    private float minimumTimePerNumber;
    
    private int newValue;
    private int previousValue;
    private IEnumerator coroutine;
    private CoroutineQueue coroutineQueue;
    
    /// <summary>
    /// <para>Adds a new coroutine to the coroutineQueue.
    /// This coroutine will change the previous value to the new value</para>
    /// </summary>
    /// <param name="value">The value will be converted to string</param>
    public void UpdateValue(int value)
    {
        previousValue = newValue;
        newValue = value;
        if (previousValue == newValue)
        {
            UpdateValueUI(previousValue);
            return;
        }
        coroutineQueue.AddCoroutine(ChangeValue());
    }

    /// <summary>
    /// <para>Changes the text of the UI from the previousValue to the newValue</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeValue()
    {
        var currentNewValue = newValue;
        if (currentNewValue == previousValue) yield break;
        var positive = currentNewValue > previousValue;
        var difference = positive ? currentNewValue - previousValue : previousValue - currentNewValue;

        var timePerNumber = Mathf.Min(timeToChangeValue / difference, minimumTimePerNumber);
        var transitionNumber = previousValue;
        var lastLoop = Time.time;
        var changes = 0;
        while (currentNewValue != transitionNumber)
        {
            var currentTime = Time.time;
            var timePassed = currentTime - lastLoop;
            lastLoop = currentTime;
            var changesQuantity = Mathf.FloorToInt(timePassed / timePerNumber);
            changes += changesQuantity;
            
            if (changes > difference) transitionNumber = currentNewValue;
            else transitionNumber += positive ? changesQuantity : -changesQuantity;
            UpdateValueUI(transitionNumber);
            
            while (Time.time < currentTime + timePerNumber)
                yield return null;
        }

        previousValue = currentNewValue;
        UpdateValueUI(previousValue);
    }

    /// <summary>
    /// <para>Changes the text of the UI to the value</para>
    /// </summary>
    /// <param name="value">The int will be converted to string</param>
    private void UpdateValueUI(int value)
    {
        valueText.text = value.ToString(CultureInfo.InvariantCulture);
    }

    private void Awake()
    {
        coroutineQueue = new CoroutineQueue(this, 5);
    }
}
