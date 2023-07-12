using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolesmovement : MonoBehaviour
{
    static public bool revv;
    public Vector3 startPosition;
    public Vector3 endPosition;
    Vector3 mposition = new Vector3(0.0f, 2.0f, -5.0f);
    public float startSpeed = 8.0f;
    Vector3 startSpeedVector;
    Vector3 speedVector;
    float speed = 11.0f;
    bool pola = false;
    bool zavrsio = false;
    float step = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        speed = startSpeed;
        startSpeedVector = new Vector3(0.0f, speed, 0.0f);
}

    private void Update()
    {
        if (revv == false)
        {
            transform.position = startPosition;
            speed = startSpeed;
            return;
        }
        
        if (pola == false)
        {
            if (transform.position.y > mposition.y)
            {
                step = speed * Time.deltaTime;
                speed -= 0.1f;
                speedVector = new Vector3(0.0f, -speed, 0.0f);
                transform.position = Vector3.SmoothDamp(transform.position, mposition, ref speedVector ,step, startSpeed);
                if (speed <= 0.0f) transform.position = mposition;
            }
			else
			{
                speed = 0.1f;
                pola = true;
            }
        }
        else
        {
            if (transform.position.x > endPosition.x)
            {
                step = speed * Time.deltaTime;
                speed += 0.003f;
                speedVector = new Vector3(-speed, 0.0f, 0.0f);
                transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref speedVector, step, startSpeed * 2.0f);
            }
            else
			{
                zavrsio = true;
                pola = false;
			}
        }
        if (zavrsio == true)
		{
            revv = false;
            PlayerMovement.zavrsio = true;
            sawbladescript.revv = false;
            transform.position = startPosition + endPosition + new Vector3(-100.0f, 100.0f, 1.0f);
            zavrsio = false;
        }
    }
}
