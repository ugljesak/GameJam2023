using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishscript : MonoBehaviour
{
    public PlayerMovement player;
    public enemyscript enemy;
    public Collider2D finishcollider;
    public int potrebanbrsrafova;
    public SpriteRenderer sr;
    public Sprite vrata;
    public Sprite blocks_12;

    void Start()
    {
        potrebanbrsrafova = 0;
        finishcollider.enabled = false;
        sr=GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (nutscript.nutCount==nutscript.maxNut)
        {
            finishcollider.enabled = true;
            sr.sprite = vrata;
        }
        else
        {
            finishcollider.enabled = false;
            sr.sprite = blocks_12;
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
}
