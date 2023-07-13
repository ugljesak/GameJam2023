using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI highscore2;

    private void Start()
    {
        highscore2.text = "HIGHSCORE: "+PlayerPrefs.GetInt("highscore2").ToString();
    }

    public void ButtonMap1()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadSceneAsync("Map1");
    }
    public void ButtonMap2()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadSceneAsync("Map2");
    }
    public void ButtonMap3()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadSceneAsync("Map3");
    }
    public void Back()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadSceneAsync(0);
    }
}
