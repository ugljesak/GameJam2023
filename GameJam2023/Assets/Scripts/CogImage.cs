using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CogImage : MonoBehaviour
{
    public Image cog;

    private void Start()
    {
        cog.enabled = false;
    }
    public void CogDisappear()
    {
        cog.enabled = false;
    }
    public void CogAppear()
    {
        cog.enabled = true;
    }
}
