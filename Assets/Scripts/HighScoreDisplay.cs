using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{

    public TextMeshProUGUI highScoreDisplay;

    void Start()
    {
        highScoreDisplay.text = "HIGH SCORE: " + HighScoreManager.instance.highScore;
    }
}