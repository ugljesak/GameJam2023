using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class enemyscript : MonoBehaviour
{
    public PlayerMovement player;
    public List<Vector2> putanja;
    public List<float> inputtime;
    float time;
    Rigidbody2D rb;
    public float ms;
    public float jurims;
    public float bezims;
    bool isjuring = false;
    int i = 0;
    int j = 0;
    bool bilazamena = false;
    float timezainput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ms = jurims;
        isjuring = false;
        time = 0;
        timezainput = 0;
        transform.position = Vector3.zero; 
    }

    void FixedUpdate()
    {
        if (bilazamena)
        {
            Move();
            if (j<inputtime.Count)
            {
                if (timezainput >= inputtime[j])
                {
                    UradiInput();
                    timezainput = 0;
                    j++;
                }
            }
        }
        time += Time.fixedDeltaTime;
        timezainput += Time.fixedDeltaTime;
    }
    
    private void Move()
    {
        if (i < putanja.Count)
        {

            rb.velocity = putanja[i] * ms;
            i++;
            
        }
    }
   
    private void UradiInput()
    {
        if (isjuring)
        {
            //baci seckalicu
        }
        else
        {
            Dash();
        }
    }


    public void ReverseRoles()
    {
        i = 0;
        j = 0;
        bilazamena = true;
        if (player.GetComponent<PlayerMovement>().putanja != null)
        {
            putanja = new List<Vector2>(player.GetComponent<PlayerMovement>().putanja);
        }
        if (player.GetComponent<PlayerMovement>().inputtime != null)
        {
            inputtime = new List<float>(player.GetComponent<PlayerMovement>().inputtime);
        }
        if (isjuring)
        {
            ms = bezims;
            isjuring = false;
        }
        else
        {
            ms = jurims;
            isjuring = true;
        }
    }

    private void Dash()
    {
        //samo animacija
    }
}
