using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Sprite Empty;
    public Sprite Apple;
    public Sprite SnakesHead;
    public Sprite SnakesBody;
    public Sprite SnakesTail;
    public Sprite SnakesBulge;
    public Sprite SnakesL;
    public Sprite SnakesLBulged;

    private Image image;

    private Sprite lastUsedImage;

    private RectTransform rectTransform;
    private TileContent content;
    private bool contentHidden;

    public RectTransform RectTransform
    {
        get
        {
            return rectTransform;
        }
    }

    public TileContent Content
    {
        get
        {
            return this.content;
        }
        set
        {
            this.content = value;
            this.ZRotation = 0;

            switch (this.content)
            {
                case TileContent.Empty:
                    this.image.sprite = this.Empty;
                    break;
                case TileContent.Apple:
                    this.image.sprite = this.Apple;
                    break;
                case TileContent.SnakesHead:
                    this.image.sprite = this.SnakesHead;
                    break;
                case TileContent.SnakesBody:
                    this.image.sprite = this.SnakesBody;
                    break;
                case TileContent.SnakesBulge:
                    this.image.sprite = this.SnakesBulge;
                    break;
                case TileContent.SnakesTail:
                    this.image.sprite = this.SnakesTail;
                    break;
                case TileContent.SnakesL:
                    this.image.sprite = this.SnakesL;
                    break;
                case TileContent.SnakesLBulged:
                    this.image.sprite = this.SnakesLBulged;
                    break;
            }

            this.lastUsedImage = this.image.sprite;
        }
    }

    private float zRotation = 0;

    public float ZRotation
    {
        get
        {
            return this.zRotation;
        }
        set
        {
            this.zRotation = value;

            transform.rotation = Quaternion.Euler(0, 0, value);
        }
    }

    public bool ContentHidden
    {
        get
        {
            return this.contentHidden;
        }
        set
        {
            this.contentHidden = value;

            if (value)
            {
                this.image.sprite = this.Empty;
            }
            else
            {
                this.image.sprite = this.lastUsedImage;
            }
        }
    }

    void Awake()
    {
        this.image = GetComponent<Image>();
        this.rectTransform = GetComponent<RectTransform>();
        this.Content = TileContent.Empty;
        this.contentHidden = false;
    }

    void Update()
    {

    }

    public void Reset()
    {
        this.Content = TileContent.Empty;
    }
}
