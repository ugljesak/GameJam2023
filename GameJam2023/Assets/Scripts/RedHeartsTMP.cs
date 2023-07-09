using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static HeartRedScript;
//using static CogImage;

public class RedHeartsTMP : MonoBehaviour
{
    public GameObject deathscreenscore;
    public DeathScreenScript DeathScreenVariable;
    HeartRedScript neededScriptHeart;
    CogImage neededScriptCog;
    public int MaxHp;
    public int CurrentHp, Increase = 0, StartingHp, CurrentPickUps;
    public bool IsHitRed, IsChangedToRun, IsChangedToShoot, IsHitBlue, PickUp;
    [SerializeField] private TextMeshProUGUI MyTextElement;
    private void Start()
    {
        MaxHp = StartingHp;
        CurrentHp = MaxHp;
        MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
        neededScriptHeart = GameObject.FindWithTag("NeededHeart").GetComponent<HeartRedScript>();
        neededScriptCog = GameObject.FindWithTag("NeededCog").GetComponent<CogImage>();
    }
    void Update()
    {
        if(PickUp == true)
        {
            CurrentPickUps++;
            MyTextElement.text = CurrentPickUps + "/" + (StartingHp + Increase - 1).ToString();
            PickUp = false;
        }
        if(IsChangedToRun == true)
        {
            Increase++;
            CurrentPickUps = 0;
            MyTextElement.text = "0/" + (StartingHp + Increase - 1).ToString();
            MyTextElement.color = Color.blue;
            CurrentHp = 1; MaxHp = 1;
            IsChangedToRun = false;
            neededScriptHeart.RedHeartDisappear();
            neededScriptCog.CogAppear();
        }
        if(IsChangedToShoot == true)
        {
            CurrentHp = StartingHp + Increase;
            MaxHp = CurrentHp;
            MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
            MyTextElement.color = Color.red;
            IsChangedToShoot = false;
            neededScriptHeart.RedHeartAppear();
            neededScriptCog.CogDisappear(); 
        }
        if(IsHitBlue == true)
        {
            Increase = 0;
            DeathScreenVariable.Setup(deathscreenscore.GetComponent<ScoreTMP>().CurrentScore);
        }
        if (IsHitRed == true)
        {
            CurrentHp--;
            MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
            IsHitRed = false;
        }
    }
}
