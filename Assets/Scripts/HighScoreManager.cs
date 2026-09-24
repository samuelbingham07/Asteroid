using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    public int highScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

 public void ScoreCompare(int score)
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }
}
    