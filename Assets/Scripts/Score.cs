using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    int score = 0;
    int highScore = 0;
    
    private void Awake(){
        instance = this;
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score", 0);
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void AddPoint(){
        score +=100;
        scoreText.text ="Score: "+ score.ToString();
        if(highScore < score)
        {
            PlayerPrefs.SetInt("High Score", score);
        }

    }
    public void AddPoint2(){
        score +=1000;
        scoreText.text ="Score: "+ score.ToString();
        if(highScore < score)
        {
            PlayerPrefs.SetInt("High Score", score);
        }
    }
}
