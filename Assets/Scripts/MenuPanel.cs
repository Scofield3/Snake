using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    private String textPrefix;
    private Text textHighScore;
    private int highScore;

    public int HighScore
    {
        get
        {
            return this.highScore;
        }
        set
        {
            this.highScore = value;
            this.textHighScore.text = this.textPrefix + value.ToString();
        }
    }

    void Awake()
    {
        this.textHighScore = transform.Find("High Score").GetComponent<Text>();
        this.textPrefix = this.textHighScore.text;
    }

    void Update()
    {

    }
}
