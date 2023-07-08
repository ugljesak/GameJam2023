using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishscript : MonoBehaviour
{
    public PlayerMovement player;
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
}
