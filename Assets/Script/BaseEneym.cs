﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected float speed;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 3;
    }
    protected void Death()
    {
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        anim.SetTrigger("death");
    }
}