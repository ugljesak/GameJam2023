using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTMP : MonoBehaviour
{
    public bool TrebaPovecatiScore;
    public int CurrentScore = 0;
    [SerializeField] private TextMeshProUGUI MyTextElement;
    void Update()
    {

        CurrentScore = PlayerMovement.score;
         MyTextElement.text = CurrentScore.ToString();
        

    }
}
