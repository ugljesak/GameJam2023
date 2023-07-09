using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreTMP : MonoBehaviour
{
    public bool TrebaPovecatiScore;
    public int CurrentScore = 0;
    [SerializeField] private TextMeshProUGUI MyTextElement;
    void Update()
    {
        if (TrebaPovecatiScore == true)
        {
            CurrentScore++;
            MyTextElement.text = CurrentScore.ToString();
            TrebaPovecatiScore = false;
        }

    }
}
