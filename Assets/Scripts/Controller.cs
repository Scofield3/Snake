using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Vector2Int lastDirection;

    public LinkedList<Vector2Int> queue;

    public Vector2Int LastDirection
    {
        get
        {
            if (this.queue.Count == 0)
                return lastDirection;

            return this.queue.Last.Value;
        }
    }

    void Start()
    {
        this.Reset();
    }

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    private float swipeThreshold = 20f;

    void Update()
    {
        if (Input.GetKeyDown("up") && this.LastDirection != Vector2.down)
        {
            this.OnUp();
        }
        else if (Input.GetKeyDown("down") && this.LastDirection != Vector2.up)
        {
            this.OnDown();
        }
        else if (Input.GetKeyDown("left") && this.LastDirection != Vector2.right)
        {
            this.OnLeft();
        }
        else if (Input.GetKeyDown("right") && this.LastDirection != Vector2.left)
        {
            this.OnRight();
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                this.fingerUp = touch.position;
                this.fingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                this.fingerDown = touch.position;
                this.CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        if (this.VerticalMove() > this.swipeThreshold && this.VerticalMove() > this.HorizontalValMove())
        {
            if (this.fingerDown.y - this.fingerUp.y > 0 && this.LastDirection != Vector2.down)
            {
                this.OnUp();
            }
            else if (this.fingerDown.y - this.fingerUp.y < 0 && this.LastDirection != Vector2.up)
            {
                this.OnDown();
            }
        }
        else if (this.HorizontalValMove() > this.swipeThreshold && this.HorizontalValMove() > this.VerticalMove())
        {
            if (this.fingerDown.x - this.fingerUp.x > 0 && this.LastDirection != Vector2.left)
            {
                this.OnRight();
            }
            else if (this.fingerDown.x - this.fingerUp.x < 0 && this.LastDirection != Vector2.right)
            {
                this.OnLeft();
            }
        }

        this.fingerUp = this.fingerDown;
    }

    private float VerticalMove()
    {
        return Math.Abs(this.fingerDown.y - this.fingerUp.y);
    }

    private float HorizontalValMove()
    {
        return Math.Abs(this.fingerDown.x - this.fingerUp.x);
    }

    private void OnUp()
    {
        this.Enqueue(Vector2Int.up);
    }

    private void OnDown()
    {
        this.Enqueue(Vector2Int.down);
    }

    private void OnLeft()
    {
        this.Enqueue(Vector2Int.left);
    }

    private void OnRight()
    {
        this.Enqueue(Vector2Int.right);
    }

    private void Enqueue(Vector2Int direction)
    {
        this.queue.AddLast(direction);
        this.lastDirection = direction;
    }

    public Vector2Int NextDirection()
    {
        if (this.queue.Count == 0)
        {
            return this.lastDirection;
        }

        var first = this.queue.First.Value;

        this.queue.RemoveFirst();

        return first;
    }

    public void Reset()
    {
        this.queue = new LinkedList<Vector2Int>();
        Enqueue(Vector2Int.up);
    }
}
