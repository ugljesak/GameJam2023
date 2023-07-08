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
        rb.velocity = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z), Mathf.Sin(transform.rotation.eulerAngles.z)) * speed;
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
