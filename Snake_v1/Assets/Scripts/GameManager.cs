using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int DifficultyMod { get; set; }
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private Slider difficultyModSlider;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        difficultyModSlider.minValue = 1;
        difficultyModSlider.maxValue = 5;
        difficultyModSlider.value = difficultyModSlider.minValue;
        
        DifficultyMod = (int)difficultyModSlider.value;

        difficultyModSlider.onValueChanged.AddListener(UpdateDifficultyText);
        difficultyModSlider.onValueChanged.AddListener(OnDifficultyModChanged);

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Difficulty", DifficultyMod);
        SceneManager.LoadScene("SnakeScene");
    }

    private void UpdateDifficultyText(float value)
    {
        float reversedValue = difficultyModSlider.maxValue - value + 1;
        difficultyText.text = "Speed increase increments: " + reversedValue.ToString("0");
    }

    private void OnDifficultyModChanged(float value)
    {
        float reversedValue = difficultyModSlider.maxValue - value + 1;
        DifficultyMod = (int)reversedValue;
    }
}
