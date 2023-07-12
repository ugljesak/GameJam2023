using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class ScoreTMP : MonoBehaviour
{
    public bool TrebaPovecatiScore;
    public int CurrentScore = 0;
    [SerializeField] private TextMeshProUGUI MyTextElement;
    void Update()
    {
        if (!PlayerMovement.isjuring)
        {
            NumberFormatInfo setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 2;
            MyTextElement.text = nutscript.timer.ToString("N",setPrecision);
        }
        else
        {
            MyTextElement.text = "";
        }


    }
}
