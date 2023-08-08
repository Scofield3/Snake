using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour 
{
    private Text textScore;
    private Text textHighScore;

    private int score;
    private int highScore;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            this.textScore.text = value.ToString();
        }
    }

    public int HighScore
    {
        get
        {
            return this.highScore;
        }
        set
        {
            this.highScore = value;
            this.textHighScore.text = value.ToString();
        }
    }

    void Awake () {
        this.textScore = this.transform.Find("Score").GetComponent<Text>();
        this.textHighScore = this.transform.Find("High Score").GetComponent<Text>();
    }
	
	void Update () {

	}
}
