using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    public PlayerMovement player;
    public Collider doorcollider;
    public int potrebanbrsrafova;
    void Start()
    {
        potrebanbrsrafova = 0;
        doorcollider.enabled = true;

    }

    private void FixedUpdate()
    {
        if (player.brsrafova == potrebanbrsrafova)
        {
            doorcollider.enabled = false;
        }

    }
}
