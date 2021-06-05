using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Opossum : BaseEnemy
{
    public Transform leftPoint;
    public Transform rightPoint;
    public LayerMask Ground;

    private float leftx;
    private float rightx;

    private bool faceLeft;

    private void Start()
    {
        base.Start();

        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        if (transform.localScale.Equals(new Vector3(1, 1, 1)))
        {
            faceLeft = true;
        }
        else
        {
            faceLeft = false;
        }
    }
    private void Update()
    {
        Move();
        FallDead();
    }
    public void Move()
    {
        if (transform.position.x <= leftx)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            faceLeft = false;
        }
        if(transform.position.x >= rightx)
        {
            transform.localScale = new Vector3(1, 1, 1);
            faceLeft = true;
        }
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

}