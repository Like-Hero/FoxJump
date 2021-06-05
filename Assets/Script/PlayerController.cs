using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Collider2D downCollider;//角色底部碰撞体（CircleCollider）
    public Collider2D topCollider;//角色头部碰撞体 （BoxCollider）

    private Rigidbody2D rb;
    private Animator anim;

    public Text cherryValue;//樱桃Text
    public Text gemValue;//宝石Text

    public LayerMask ground;//地面

    public AudioSource hurtAudio;//受伤音效
    public AudioSource jumpAudio;//跳跃音效
    public AudioSource CollectionAudio;//拾取物品音效

    public Transform cellingPoint;//顶部判断点，判断头上是否有东西，防止蹲下然后站立的时候穿模
    public Transform groundCheck;//底部判断点,判断脚下是否踩着地面
    public Transform deadPoint;//死亡点

    public float speed;//速度
    public float jumpforce;//跳跃的力

    private bool isGround;//是否在地面上
    private bool isJump;//是否跳起
    private bool jumpPressed;//是否按下跳跃键
    private int jumpCount;//几段跳

    public const float CROUCH_RATE = 0.3f;//蹲下之后速度的比例
    public const float MAX_FALL_SPEED = -20.0f;//最大下落速度，防止帧数不够导致穿模
    public const int MAX_JUMP_COUNT = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        downCollider = GetComponent<CircleCollider2D>();
    }

    //Update用于实时判断
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
        UpdateCollectionValue();
    }

    //FixedUpdate用于物理变换，例如角色运动以及刚体变换
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        //没有死亡并且没有受伤才可以控制主角
        if (!JudgeDead() && !anim.GetBool("hurt"))
        {
            Move();
        }
        SwitchAnim();
    }
    
    private void UpdateCollectionValue()
    {
        cherryValue.text = PlayerData.CheeryCount.ToString();
        gemValue.text = PlayerData.GemCount.ToString();
    }
    private void Move()
    {
        XMove();
        YMove();
    }
    private void XMove()
    {
        float xVelocity = Input.GetAxisRaw("Horizontal");
        //改变朝向
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }
        //改变x速度,如果趴下状态,则改变为趴下的速度
        if (Crouch())
        {
            rb.velocity = new Vector2(xVelocity * speed * CROUCH_RATE, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        }
    }
    private void YMove()
    {
        if (isGround)
        {
            jumpCount = MAX_JUMP_COUNT;//恢复最大跳跃数
            isJump = false;
        }
        if(jumpPressed && jumpCount > 0 && !anim.GetBool("crouching"))
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            if(rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, MAX_FALL_SPEED));
            }
            jumpCount--;
            jumpPressed = false;
        }
    }
    private void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        if (isGround)
        {
            anim.SetBool("falling", false);
        }else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        else if(rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        //下蹲动画
        //如果不是跳跃状态并且不是下落状态
        if(!anim.GetBool("jumping") && !anim.GetBool("falling"))
        {
            //头上没东西
            if (!Physics2D.OverlapCircle(cellingPoint.position, 0.2f, ground))
            {
                if (Input.GetButton("Crouch"))
                {
                    anim.SetBool("crouching", true);
                    topCollider.enabled = false;
                }
                else
                {
                    anim.SetBool("crouching", false);
                    topCollider.enabled = true;
                }
            }
        }
    }
    private bool JudgeDead()
    {
        if (transform.position.y < deadPoint.position.y)
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Dead", 2.0f);
            return true;
        }
        return false;
    }
    private void Dead()
    {
        //恢复Collection数值
        PlayerData.CheeryCount = PlayerData.PrevCheeryCount;
        PlayerData.GemCount = PlayerData.PrevGemCount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Hurt(GameObject enemy)
    {
        rb.velocity = new Vector2(enemy.transform.position.x < this.transform.position.x ? 10 : -10, jumpforce / 2);
        hurtAudio.Play();
    }
    private bool Crouch()
    {
        //如果是跳跃或者下落,则不可下蹲
        if (anim.GetBool("jumping") || anim.GetBool("falling"))
        {
            return false;
        }
        else
        {
            //头上没东西
            if (!Physics2D.OverlapCircle(cellingPoint.position, 0.2f, ground))
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        //捡到物品
        if (other.gameObject.CompareTag("Cherry"))
        {
            PlayerData.CheeryCount++;
            CollectionAudio.Play();
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Gem"))
        {
            PlayerData.GemCount++;
            CollectionAudio.Play();
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
    
}