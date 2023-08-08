using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private String textPrefix;

    private Text textScore;

    private int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            this.textScore.text = this.textPrefix + value.ToString();
        }
    }

    void Awake()
    {
        this.textScore = this.transform.Find("Score").GetComponent<Text>();
        
        this.textPrefix = this.textScore.text;
    }

    void Update()
    {

    }
}