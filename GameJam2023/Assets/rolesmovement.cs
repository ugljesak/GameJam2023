using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolesmovement : MonoBehaviour
{
    static public bool revv;
    public Vector3 startPosition;
    public Vector3 endPosition;
    Vector3 mposition = new Vector3(0.0f, 2.0f, -1.0f);
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        speed = 50.0f;
}

    private void Update()
    {
        if (revv == true)
        {
            float step = 0;
            while (transform.position.y > mposition.y)
            {
                print(transform.position);
                step = speed * Time.deltaTime;
                speed -= 0.1f;
                transform.position = Vector3.MoveTowards(transform.position, mposition, step);
                if (speed <= 0.0f) transform.position = mposition;
            }
            speed = 0.1f;
            while (transform.position.x < endPosition.x)
            {
                step = speed * Time.deltaTime;
                speed += 0.1f;
                transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            }
            transform.position = startPosition;

            revv = false;
        }
    }

    
}
