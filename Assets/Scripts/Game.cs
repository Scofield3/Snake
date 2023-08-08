using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float time;
    private SoundManager soundManager;
    private Snake snake;
    private Vector2Int applePosition;
    private Controller controller;

    public MenuPanel Menu;
    public GameOverPanel GameOver;
    public GamePanel GamePanel;
    public ScoresPanel ScoresPanel;

    [Range(0f, 3f)]
    public float GameSpeed;

    public Board Board;

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
            this.GamePanel.Score = value;
            this.GameOver.Score = value;

            if (value > this.HighScore)
            {
                this.HighScore = value;
            }
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
            PlayerPrefs.SetInt("High Score", value);
            this.GamePanel.HighScore = value;
            this.Menu.HighScore = value;
        }
    }

    public bool Paused { get; private set; }

    void Start()
    {
        this.HighScore = PlayerPrefs.GetInt("High Score", 0);
        
        this.ShowMenu();

        this.controller = GetComponent<Controller>();

        this.snake = new Snake(this.Board);

        this.Paused = true;

        this.soundManager = GetComponent<SoundManager>();
    }

    void Update()
    {
        this.time += Time.deltaTime;

        while (this.time > this.GameSpeed)
        {
            this.time -= this.GameSpeed;
            
            this.UpdateGameState();
        }
    }

    private void UpdateGameState()
    {
        if (!this.Paused && this.snake != null)
        {
            var direction = this.controller.NextDirection();

            var head = this.snake.NextHeadPosition(direction);

            var x = head.x;
            var y = head.y;

            if (this.snake.WithoutTail.Contains(head))
            {
                this.StartCoroutine(this.GameOverCoroutine());
                return;
            }

            if (x >= 0 && x < Board.Columns && y >= 0 && y < Board.Rows)
            {
                if (head == applePosition)
                {
                    this.soundManager.PlayAppleSoundEffect();

                    this.snake.Move(direction, true);

                    this.Score += 1;
                    
                    this.PlantAnApple();
                }
                else
                {
                    snake.Move(direction, false);
                }
            }
            else
            {
                this.StartCoroutine(this.GameOverCoroutine());
            }
        }
    }

    public void ShowMenu()
    {
        this.HideAllPanels();

        this.Menu.gameObject.SetActive(true);
    }

    public void ShowGameOver()
    {
        this.HideAllPanels();

        this.GameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        this.HideAllPanels();

        this.Restart();
        
        this.GamePanel.gameObject.SetActive(true);
    }

    public void ShowScores()
    {
        this.HideAllPanels();

        this.ScoresPanel.gameObject.SetActive(true);
    }

    private void HideAllPanels()
    {
        this.Menu.gameObject.SetActive(false);
        this.GamePanel.gameObject.SetActive(false);
        this.GameOver.gameObject.SetActive(false);
        this.ScoresPanel.gameObject.SetActive(false);
    }

    private void Restart()
    {
        this.controller.Reset();

        this.Score = 0;

        this.Board.Reset();

        this.snake.Reset();

        this.PlantAnApple();

        this.Paused = false;
        this.time = 0;
    }

    private void PlantAnApple()
    {
        if (this.Board[this.applePosition].Content == TileContent.Apple)
        {
            this.Board[this.applePosition].Content = TileContent.Empty;
        }

        var emptyPositions = this.Board.EmptyPositions.ToList();

        if (emptyPositions.Count == 0)
        {
            return;
        }

        int position = Random.Range(0, emptyPositions.Count);

        this.applePosition = emptyPositions[position];
        this.Board[this.applePosition].Content = TileContent.Apple;
    }

    private IEnumerator GameOverCoroutine()
    {
        this.soundManager.PlayGameOverSoundEffect();

        this.Paused = true;

        for (int i = 0; i < 3; i++)
        {
            this.snake.Hide();
            yield return new WaitForSeconds(GameSpeed * 1.5f);

            this.snake.Show();
            yield return new WaitForSeconds(GameSpeed * 1.5f);
        }

        this.ShowGameOver();
    }
}
