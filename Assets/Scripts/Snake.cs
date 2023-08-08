using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : IEnumerable<Vector2Int>
{
    private LinkedList<Vector2Int> body;
    private HashSet<Vector2Int> bulges;

    private Board board;

    public Vector2Int Head
    {
        get
        {
            return this.body.Last.Value;
        }
    }

    public IEnumerable<Vector2Int> WithoutTail
    {
        get
        {
            return this.Where((p) => { return p != this.body.First.Value; });
        }
    }

    public Snake(Board board)
    {
        this.board = board;
        this.body = new LinkedList<Vector2Int>();
        this.bulges = new HashSet<Vector2Int>();

        this.Reset();
    }

    public void Hide()
    {
        foreach (var p in this.body)
        {
            this.board[p].ContentHidden = true;
        }
    }

    public void Show()
    {
        foreach (var p in this.body)
        {
            this.board[p].ContentHidden = false;
        }
    }

    public void Reset()
    {
        foreach (var p in this.body)
        {
            this.board[p].Content = TileContent.Empty;
        }

        this.body.Clear();
        this.bulges.Clear();

        var start = new Vector2Int(5, 13);
        for (int i = 0; i < 5; i++)
        {
            var position = new Vector2Int(start.x, start.y - i);
            this.body.AddLast(position);
        }

        this.UpdateSnakeState();
    }

    public void Move(Vector2Int direction, bool extend)
    {
        var newHead = this.NextHeadPosition(direction);

        this.body.AddLast(newHead);

        if (extend)
        {
            this.bulges.Add(newHead);
        }
        else
        {
            this.bulges.Remove(this.body.First.Value);
            this.board[this.body.First.Value].Content = TileContent.Empty;
            this.body.RemoveFirst();
        }

        this.UpdateSnakeState();
    }

    private void UpdateSnakeState()
    {
        //handle head
        var headPosition = this.body.Last.Value;
        var nextPosition = this.body.Last.Previous.Value;

        var tile = this.board[headPosition];
        tile.Content = TileContent.SnakesHead;

        if (nextPosition.y > headPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (nextPosition.y < headPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (nextPosition.x > headPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (nextPosition.x < headPosition.x)
        {
            tile.ZRotation = -90;
        }

        //handle snake parts
        var previous = this.body.Last;
        var current = this.body.Last.Previous;

        while (current != this.body.First)
        {
            var next = current.Previous;
            tile = this.board[current.Value];
            if (previous.Value.x == next.Value.x)
            {
                if (this.bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesBulge;
                }
                else
                {
                    tile.Content = TileContent.SnakesBody;
                }
                tile.ZRotation = 0;
            }
            else if (previous.Value.y == next.Value.y)
            {
                if (this.bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesBulge;
                }
                else
                {
                    tile.Content = TileContent.SnakesBody;
                }
                tile.ZRotation = 90;
            }
            else
            {
                if (this.bulges.Contains(current.Value))
                {
                    tile.Content = TileContent.SnakesLBulged;
                }
                else
                {
                    tile.Content = TileContent.SnakesL;
                }
                if ((previous.Value.x > current.Value.x && next.Value.y < current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 0;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y < current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y < current.Value.y))
                {
                    tile.ZRotation = 90;
                }
                else if ((previous.Value.x < current.Value.x && next.Value.y > current.Value.y) || (next.Value.x < current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 180;
                }
                else if ((previous.Value.x > current.Value.x && next.Value.y > current.Value.y) || (next.Value.x > current.Value.x && previous.Value.y > current.Value.y))
                {
                    tile.ZRotation = 270;
                }
                else
                {
                    tile.Content = TileContent.SnakesHead;
                }
            }
            previous = current;
            current = current.Previous;
        }

        //handle tail
        var tailPosition = this.body.First.Value;
        var previousPosition = this.body.First.Next.Value;

        tile = this.board[tailPosition];
        tile.Content = TileContent.SnakesTail;

        if (previousPosition.y > tailPosition.y)
        {
            tile.ZRotation = 0;
        }
        else if (previousPosition.y < tailPosition.y)
        {
            tile.ZRotation = 180;
        }
        else if (previousPosition.x > tailPosition.x)
        {
            tile.ZRotation = 90;
        }
        else if (previousPosition.x < tailPosition.x)
        {
            tile.ZRotation = -90;
        }
    }

    public Vector2Int NextHeadPosition(Vector2Int direction)
    {
        return this.Head + new Vector2Int(direction.x, -direction.y);
    }

    public IEnumerator<Vector2Int> GetEnumerator()
    {
        return ((IEnumerable<Vector2Int>)this.body).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Vector2Int>)this.body).GetEnumerator();
    }
}
