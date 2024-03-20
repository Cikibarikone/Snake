using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    public static ResultsManager Instance { get; private set; }

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI personalScoreText;
    public Button restartButton;
    public Button mainMenuButton;

    private void Start()
    {
        int difficulty = GameManager.Instance.DifficultyMod;
        personalScoreText.text = "" + PlayerPrefs.GetInt("Points" + difficulty, 0);
        highScoreText.text = "" + PlayerPrefs.GetInt("HighscoreText" + difficulty, 0);

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartGame);

        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(BackToMainMenu);
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Points" + GameManager.Instance.DifficultyMod, 0);
        SceneManager.LoadScene("SnakeScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
