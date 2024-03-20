using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FoodPickups : MonoBehaviour
{

    public static FoodPickups instance;
    
    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;

    public Snake snake;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }      

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "SCORE: " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highscoreText.text = "HIGHSCORE: " + score.ToString();
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    { 
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
    }

}
