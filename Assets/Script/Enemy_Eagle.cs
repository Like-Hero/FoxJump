using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : BaseEnemy
{
    public Transform upPoint;
    public Transform downPoint;

    public float speed;

    private float upY;
    private float downY;

    private bool faceUp;

    private void Start()
    {
        base.Start();

        upY = upPoint.position.y;
        downY = downPoint.position.y;

        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);

    }
    private void Update()
    {
        if (GameManager.Ins.isPause)
        {
            rb.velocity = Vector2.zero;
            return;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Ins.isPause) return;
        Move();
    }
    private void Move()
    {
        if (faceUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y >= upY)
            {
                faceUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y <= downY)
            {
                faceUp = true;
            }
        }
    }
}
