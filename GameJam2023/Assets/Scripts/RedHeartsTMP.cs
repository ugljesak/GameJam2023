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



        CurrentPickUps = nutscript.nutCount;
        MyTextElement.text = CurrentPickUps + "/" + (StartingHp + Increase - 1).ToString();
            
        if(!PlayerMovement.isjuring)
        {
            Increase++;
            CurrentPickUps = 0;
            MyTextElement.text = "0/" + (StartingHp + Increase - 1).ToString();
            MyTextElement.color = Color.black;
            CurrentHp = 1; MaxHp = 1;
            IsChangedToRun = false;
            neededScriptHeart.RedHeartDisappear();
            neededScriptCog.CogAppear();
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
        }
        if(PlayerMovement.health == 0)
        {
            DeathScreenVariable.Setup(deathscreenscore.GetComponent<ScoreTMP>().CurrentScore);
        }
        CurrentHp = enemyscript.health;
        MyTextElement.text = CurrentHp.ToString() + "/" + MaxHp.ToString();
 
    }
}
