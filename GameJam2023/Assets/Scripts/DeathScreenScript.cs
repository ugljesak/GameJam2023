using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    public TextMeshProUGUI scorepoints;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scorepoints.text = score.ToString();
    }
    public void RestartButton() {
        gameObject.GetComponent<AudioSource>().Play();
        string c = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(c);
    }
    public void ExitButton()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("DodosMenu"); //
    }
}
