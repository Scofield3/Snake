using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresPanel : MonoBehaviour
{
    //private List<Score> scoreboard = new List<Score>();
    private int maxScoresCount = 10;


    public List<int> scores = new List<int>();
    public List<string> names = new List<string>();

    private int highscoreToSet;
    private int tempScore;
    private string tempName;
    private string scoreList;
    private string socreNameList;

    public void SetHighScore(string name, int score)
    {

    }

    void Awake()
    {
        
    }

    void Update()
    {

    }
}
