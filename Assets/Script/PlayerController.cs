using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Collider2D downCollider;
    public Collider2D topCollider;

    private Rigidbody2D rb;
    private Animator anim;
    public Text cherryValue;
    public LayerMask ground;
    public LayerMask obstruct;


    public AudioSource hurtAudio;
    public AudioSource jumpAudio;
    public AudioSource cherryAudio;

    public Transform cellingPoint;

    public float speed;
    public float jumpforce;
    public int cherryAmount;

    public const float CROUCH_RATE = 0.3f;

    private void Start()
    {
        speed = 10;
        jumpforce = 15;
        cherryAmount = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        downCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        //受伤了，玩家失去对主角的控制权
        if (!anim.GetBool("hurt"))
        {
            Move();
        }
        UpdateCherryValue();
    }

    private void UpdateCherryValue()
    {
        cherryValue.text = cherryAmount.ToString();
    }

    private void Move()
    {
        XMove();
        YMove();
    }
    private void XMove()
    {
        float xVelocity = Input.GetAxis("Horizontal");
        float facedDir = Input.GetAxisRaw("Horizontal");
        //改变朝向
        if (facedDir != 0)
        {
            transform.localScale = new Vector3(facedDir, 1, 1);
        }
        //改变x速度,如果为真,则改变速度
        if (Crouch())
        {
            rb.velocity = new Vector2(xVelocity * speed * CROUCH_RATE, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(xVelocity * CROUCH_RATE));
        }
        else
        {
            anim.SetFloat("running", Mathf.Abs(xVelocity));
            if (xVelocity != 0)
            {
                rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
            }
        }
    }
    private void YMove()
    {
        //改变y速度
        if (Input.GetButtonDown("Jump") && (downCollider.IsTouchingLayers(ground) || downCollider.IsTouchingLayers(obstruct)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("falling", false);
            anim.SetBool("jumping", true);
            jumpAudio.Play();
            //anim.SetBool("idle", false);
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
            //anim.SetBool("idle", false);
        }
        if (downCollider.IsTouchingLayers(ground) || downCollider.IsTouchingLayers(obstruct))
        {
            anim.SetBool("falling", false);
            anim.SetBool("hurt", false);//掉落在地上恢复玩家对主角的控制权
            //anim.SetBool("idle", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //捡到物品
        if (other.gameObject.CompareTag("Collection"))
        {
            cherryAmount++;
            cherryAudio.Play();
            Destroy(other.gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //遇到敌人
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(transform.position.y - 1 > other.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
                BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
                enemy.JumpOn();
            }
            else
            {
                anim.SetBool("hurt", true);
                Hurt(other.gameObject);
                print("撞到敌人");
            }
        }
        if (other.gameObject.CompareTag("ground"))
        {
            anim.SetBool("hurt", false);
            anim.SetBool("jumping", false);
        }
    }
    private void Hurt(GameObject enemy)
    {
        rb.velocity = new Vector2(enemy.transform.position.x < this.transform.position.x ? 10 : -10, jumpforce / 2);
        hurtAudio.Play();
    }
    private bool Crouch()
    {
        //如果是跳跃或者下落,则不可下蹲
        if(anim.GetBool("jumping") || anim.GetBool("falling"))
        {
            return false;
        }
        else
        {
            //头上没东西
            if (!Physics2D.OverlapCircle(cellingPoint.position, 0.2f, obstruct))
            {
                if (Input.GetButton("Crouch"))
                {
                    anim.SetBool("crouching", true);
                    topCollider.enabled = false;
                    return true;
                }
                else
                {
                    anim.SetBool("crouching", false);
                    topCollider.enabled = true;
                    return false;
                }
            }
            else//头上有东西
            {
                print("OK");
                //进一步判断是否是蹲下状态
                if (anim.GetBool("crouching"))
                {
                    
                    topCollider.enabled = false;
                    return true;
                }
                else
                {
                    topCollider.enabled = true;
                    return false;
                }
            }
        }
        
    }
}