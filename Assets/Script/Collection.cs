using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    private Animator anim;
    public AudioSource CollectionAudio;//拾取物品音效

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PickCollection(string collectionName)
    {
        if (collectionName.Equals("Cherry"))
        {
            PlayerData.CheeryCount++;
        }
        else if(collectionName.Equals("Gem"))
        {
            PlayerData.GemCount++;
        }
        else
        {
            print("PickCollection Error");
            return;
        }
        CollectionAudio.Play();
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("pick");
    }
    private void Update()
    {
        if (GameManager.Ins.isPause) return;
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
}
