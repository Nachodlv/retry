using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// <para>Updates the value shown in valueText</para>
    /// </summary>
    /// <param name="value">The value will be converted to string</param>
    public void UpdateValue(float value)
    {
        valueText.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
