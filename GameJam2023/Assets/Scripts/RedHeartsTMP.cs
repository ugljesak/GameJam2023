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
    public int CurrentHp, Increase = 0, StartingHp, CurrentPickUps, MaxPickUps;
    public bool IsHitRed, IsChangedToRun, IsChangedToShoot, IsHitBlue, PickUp;
    [SerializeField] private TextMeshProUGUI MyTextElement;
    private void Start()
    {
        MaxHp = StartingHp;
        CurrentHp = MaxHp;
        MyTextElement.text = "0/1";
        neededScriptHeart = GameObject.FindWithTag("NeededHeart").GetComponent<HeartRedScript>();
        neededScriptCog = GameObject.FindWithTag("NeededCog").GetComponent<CogImage>();
    }
    void Update()
    {


            
        if(!PlayerMovement.isjuring)
        {
            MyTextElement.color = Color.black;
            neededScriptHeart.RedHeartDisappear();
            neededScriptCog.CogAppear();
            CurrentPickUps = nutscript.nutCount;
            MaxPickUps = nutscript.maxNut;
            MyTextElement.text = CurrentPickUps + "/" + MaxPickUps.ToString();

        }
        if(PlayerMovement.isjuring)
        {
            CurrentHp = StartingHp + Increase;
            MaxHp = CurrentHp;
            MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
            MyTextElement.color = new Color(1f, 0.38f, 0.38f, 1f);
            IsChangedToShoot = false;
            neededScriptHeart.RedHeartAppear();
            neededScriptCog.CogDisappear();
            CurrentHp = enemyscript.health;
            MaxHp = enemyscript.maxhealth - 1;
            MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
        }
        if(PlayerMovement.health == 0)
        {
            DeathScreenVariable.Setup(deathscreenscore.GetComponent<ScoreTMP>().CurrentScore);
        }
 
    }
}
