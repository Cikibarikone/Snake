using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public static string SpeedText { get; private set; }


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int points = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints()
    {
        points++;
        PlayerPrefs.SetInt("Points" + GameManager.Instance.DifficultyMod, points);
        UpdateScoreText();
        CheckHighScore();
    }

    public void ResetScore()
    {
        points = 0;
        PlayerPrefs.SetInt("Points" + GameManager.Instance.DifficultyMod, points);
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void CheckHighScore()
    {
        if (points > PlayerPrefs.GetInt("HighscoreText" + GameManager.Instance.DifficultyMod, 0))
        {
            PlayerPrefs.SetInt("HighscoreText" + GameManager.Instance.DifficultyMod, points);
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"{points}";
    }

    public void UpdateHighScoreText()
    {
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreText" + GameManager.Instance.DifficultyMod, 0);
    }
}
