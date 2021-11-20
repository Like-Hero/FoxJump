using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Eagle,
    Frog,
    Opossum
}
public class BaseEnemy : MonoBehaviour
{
    protected float speed;

    protected Rigidbody2D rb;
    protected Animator anim;

    public Transform deadPoint;

    public AudioSource deathAudio;
    public EnemyType enemyType;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 3;
    }
    protected void FallDead()
    {
        if (transform.position.y < deadPoint.position.y)
        {
            Death();
        }
    }
    protected void Death()
    {
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        //为了防止连续爆炸和敌人还可以走动的问题
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        deathAudio.Play();
        anim.SetTrigger("death");
    }
}
