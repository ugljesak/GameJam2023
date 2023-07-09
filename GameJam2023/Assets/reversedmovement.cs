using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversedmovement : MonoBehaviour
{
    static public bool revv;
    public Vector3 startPosition;
    public Vector3 endPosition;
    Vector3 mposition = new Vector3(0.0f, -2.0f, -1.0f);
    public float speed = 12.0f;
    bool pola = false;
    bool zavrsio = false;
    float step = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        speed = 12.0f;
    }

    private void Update()
    {
        if (revv == false)
        {
            transform.position = startPosition;
            speed = 12.0f;
            return;
        }

        if (pola == false)
        {
            if (transform.position.y < mposition.y)
            {
                print(transform.position);
                step = speed * Time.deltaTime;
                speed -= 0.06f;
                transform.position = Vector3.MoveTowards(transform.position, mposition, step);
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
            if (transform.position.x < endPosition.x)
            {
                step = speed * Time.deltaTime;
                speed += 0.06f;
                transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            }
            else
            {
                zavrsio = true;
                pola = false;
            }
        }
        if (zavrsio == true)
        {
            transform.position = startPosition + endPosition + new Vector3(100.0f, -100.0f, 1.0f);
            zavrsio = false;
        }
    }
}
