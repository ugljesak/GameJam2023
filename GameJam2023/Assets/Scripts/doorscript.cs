using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    public Collider doorcollider;

    void Start()
    {
        doorcollider.enabled = true;

    }

    private void FixedUpdate()
    {
        if (nutscript.nutCount==nutscript.maxNut)
        {
            doorcollider.enabled = false;
        }

    }
}
