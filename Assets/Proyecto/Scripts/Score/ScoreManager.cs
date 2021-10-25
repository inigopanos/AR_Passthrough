using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int highScore;
    public int currScore;
    public Text scoreText;

    void Start()
    {
        currScore = highScore;
    }

    void Update()
    {
        scoreText.text = "Score: " + currScore;
    }

    public void SumarPuntos(int puntos)
    {
        currScore += puntos;
        print(currScore);
    }
}
