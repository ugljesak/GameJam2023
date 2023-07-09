using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1ButtonScript : MonoBehaviour
{
    public void ButtonMap1()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Map1");
    }
    public void ButtonMap2()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Map2");
    }
    public void ButtonMap3()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Map3");
    }
}
