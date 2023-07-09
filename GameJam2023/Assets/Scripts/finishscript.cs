using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishscript : MonoBehaviour
{
    public PlayerMovement player;
    public enemyscript enemy;
    public Collider2D finishcollider;
    public int potrebanbrsrafova;
    void Start()
    {
        potrebanbrsrafova = 0;
        finishcollider.enabled = false;

    }

    private void FixedUpdate()
    {
        if (player.brsrafova == potrebanbrsrafova)
        {
            finishcollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //&& !PlayerMovement.isjuring)
        {
            print("tusam");
            enemy.ReverseRoles();
            player.ReverseRoles();
        }
        if(collision.gameObject.tag=="enemy" && !enemy.isjuring)
        {
            print("izgubio si");
        }
    }
}
