using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishscript : MonoBehaviour
{
    public PlayerMovement player;
    public enemyscript enemy;
    public Collider2D finishcollider;
    public Animator animator;
    string curanim;
    public bool dosoportal = false;

    void Start()
    {
        finishcollider.enabled = false;
        curanim = "empty";
    }

    void ChangeAnimation(string newanim)
    {
        if (!CanChangeAnimation(newanim)) return;
        animator.Play(newanim);
        curanim = newanim;
    }

    bool CanChangeAnimation(string newanim)
    {
        if(newanim == curanim) return false;
        return true;
    }

    private void FixedUpdate()
    {
        if (nutscript.nutCount==nutscript.maxNut)
        {
            finishcollider.enabled = true;
            if (!dosoportal) 
            { 
                ChangeAnimation("portalspawn");
                dosoportal = true; 
            }
        }
        else
        {
            finishcollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !PlayerMovement.isjuring)
        {
            enemy.ReverseRoles();
            player.ReverseRoles();
        }
        if(collision.gameObject.tag=="enemy" && !enemy.isjuring)
        {
            PlayerMovement.health = 0;
        }
    }

  

    public void EndSpawn()
    {
        print("ASDAAA");
        ChangeAnimation("portal");
    }
}
