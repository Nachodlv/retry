using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField][Tooltip("Text where the score will appear")]
    private TextMeshProUGUI scoreText;

    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
    }
}
