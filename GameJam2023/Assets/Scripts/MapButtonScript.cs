using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1ButtonScript : MonoBehaviour
{
    public void ButtonMap1()
    {
        SceneManager.LoadScene("Map1");
    }
    public void ButtonMap2()
    {
        SceneManager.LoadScene("Map2");
    }
    public void ButtonMap3()
    {
        SceneManager.LoadScene("Map3");
    }
}
