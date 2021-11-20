using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PickCollection(string collectionName)
    {
        if (collectionName.Equals("Cherry"))
        {
            GameManager.Ins.currentPassData.cherry++;
        }
        else if(collectionName.Equals("Gem"))
        {
            GameManager.Ins.currentPassData.gem++;
        }
        else
        {
            print("PickCollection Error");
            return;
        }
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("pick");
    }
    private void Update()
    {
        if (MenuController.Ins.isPause) return;
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
}
