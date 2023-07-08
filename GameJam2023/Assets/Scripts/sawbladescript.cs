using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawbladescript : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rb;
    public float speed;
    float time;
    public float lifeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //print(transform.rotation.eulerAngles.z);
        float angle = transform.rotation.eulerAngles.z;
        if (angle % 360 < 180)
		{
            rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
		}
		else
		{
            angle -= 360;
            rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
		}
        time = Time.time;
    }

    void Update()
    {
        if(Time.time - time >= lifeTime)
		{
            Destroy(gameObject);
		}
    }
}
