using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

/// <summary>
/// <para>Displays a float in a TextMeshProUGUI</para>
/// </summary>
public class ShowNumberUI : MonoBehaviour
{
    [SerializeField][Tooltip("Text where the value will appear")]
    private TextMeshProUGUI valueText;

    [SerializeField] [Tooltip("Time it takes to change to a new value")]
    private float timeToChangeValue = 1;
    
    private int newValue;
    private int previousValue;
    private IEnumerator coroutine;
    
    /// <summary>
    /// <para>Starts a coroutine to change the value of the UI. The coroutine makes a transition between the old
    /// value and the new value</para>
    /// </summary>
    /// <param name="value">The value will be converted to string</param>
    public void UpdateValue(int value)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            previousValue = newValue;
        }

        newValue = value;
        if (previousValue == newValue)
        {
            UpdateValueUI(previousValue);
            return;
        }
        coroutine = ChangeValue();
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// <para>Changes the text of the UI from the previousValue to the newValue</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeValue()
    {
        if (newValue == previousValue) yield break;
        var positive = newValue > previousValue;
        var difference = positive ? newValue - previousValue : previousValue - newValue;

        var timePerNumber = timeToChangeValue / difference;
        var transitionNumber = previousValue;
        while (newValue != transitionNumber)
        {
            transitionNumber += positive ? 1 : -1;
            UpdateValueUI(transitionNumber);
            var currentTime = Time.time;
            while (Time.time < currentTime + timePerNumber)
                yield return null;
        }

        previousValue = newValue;
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
}
