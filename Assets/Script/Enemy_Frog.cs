﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour, Enemy_Land_Base
{
    public Transform leftPoint;
    public Transform rightPoint;
    public LayerMask Ground;

    private float leftx;
    private float rightx;


    public float speed;
    public float jumpForce;


    private bool faceLeft;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        
        if (transform.localScale.Equals(new Vector3(1, 1, 1)))
        {
            faceLeft = true;
        }
    }
    
    private void Update()
    {
        SwitchAnim();
    }

    public void Move()
    {
        if (transform.position.x <= leftx)
        {
            faceLeft = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(transform.position.x >= rightx)
        {
            transform.localScale = new Vector3(1, 1, 1);
            faceLeft = true;
        }

        if (faceLeft)
        {
            if (collider.IsTouchingLayers(Ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            
        }
        else
        {
            if (collider.IsTouchingLayers(Ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
        }
    }
    private void SwitchAnim()
    {
        if (anim.GetBool("jumping"))//跳跃
        {
            if(rb.velocity.y < 0.1f)//下落
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (collider.IsTouchingLayers(Ground) && anim.GetBool("falling"))//落地
        {
            anim.SetBool("falling", false);
        }
    }

}