using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour, IEnumerable<Tile>
{
    private RectTransform rectTransform;
    private List<Tile> tiles;

    public GameObject TilePrefab;

    [Range(0, 30)]
    public int Columns = 10;

    [Range(0, 30)]
    public int Rows = 15;

    [Range(0, 20f)]
    public float Margins = 3;

    public IEnumerable<Vector2Int> Positions
    {
        get
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < this.Rows; i++)
            {
                x = 0;
                for (int j = 0; j < this.Columns; j++)
                {
                    yield return new Vector2Int(x, y);

                    x++;
                }

                y++;
            }
        }
    }

    public IEnumerable<Vector2Int> EmptyPositions
    {
        get
        {
            return this.Positions.Where((p) => { return this[p].Content == TileContent.Empty; });
        }
    }

    void Awake()
    {
        this.rectTransform = transform as RectTransform;

        var width = this.rectTransform.rect.width;
        var tileSize = (width - this.Margins * 2) / this.Columns;
        var halfTileSize = tileSize / 2;

        this.rectTransform.sizeDelta = new Vector2(width, tileSize * this.Rows + this.Margins * 2);

        this.tiles = new List<Tile>();

        float x = this.Margins;
        float y = this.Margins;

        for (int i = 0; i < this.Rows; i++)
        {
            x = this.Margins;
            for (int j = 0; j < this.Columns; j++)
            {
                var tile = Instantiate(this.TilePrefab, new Vector3(x + halfTileSize, -y - halfTileSize, 0), Quaternion.identity).GetComponent<Tile>();
                
                tile.transform.SetParent(rectTransform, false);
                tile.RectTransform.sizeDelta = new Vector2(tileSize, tileSize);

                this.tiles.Add(tile);

                x += tileSize;
            }

            y += tileSize;
        }

        this[5, 5].Content = TileContent.Apple;
    }

    void Update()
    {

    }

    public IEnumerator<Tile> GetEnumerator()
    {
        return ((IEnumerable<Tile>)this.tiles).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Tile>)this.tiles).GetEnumerator();
    }

    public Tile this[int x, int y]
    {
        get
        {
            if (!(x >= 0 && x < Columns))
            {
                throw new System.ArgumentOutOfRangeException("x", "x coordinate must be between 0 and the number of columns.");
            }

            if (!(y >= 0 && y < Rows))
            {
                throw new System.ArgumentOutOfRangeException("y", "y coordinate must be between 0 and the number of rows.");
            }

            return tiles[y * Columns + x];
        }
    }

    public Tile this[Vector2Int position]
    {
        get
        {
            return this[position.x, position.y];
        }
    }

    public void Reset()
    {
        foreach (var tile in this)
        {
            tile.Reset();
        }
    }
}
