using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartRedScript : MonoBehaviour
{
    public Image heart;
    public void RedHeartDisappear()
    {
        heart.enabled = false;
    }
    public void RedHeartAppear()
    {
        heart.enabled = true;
    }

}
