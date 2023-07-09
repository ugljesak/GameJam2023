using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitButton()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Application.Quit();
        Debug.Log("Game Closed");
    }
    public void StartGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(delay());
        IEnumerator delay()
        {
            yield return new WaitForSeconds(1f);
        }
        delay();
        SceneManager.LoadSceneAsync("MapScene");
    }
}
