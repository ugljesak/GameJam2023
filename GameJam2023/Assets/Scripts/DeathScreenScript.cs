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
        SceneManager.LoadScene("Dodos"); //staviti koja scena je igrica
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("DodosMenu"); //
    }
}
