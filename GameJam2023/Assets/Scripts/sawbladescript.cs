using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawbladescript : MonoBehaviour
{
    Vector2 currentVelocity;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Wall")
		{
            currentVelocity = rb.velocity;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Wall")
        {
            int signX = 0, signY = 0;
            if(Mathf.Abs(currentVelocity.x - rb.velocity.x) < Mathf.Abs(currentVelocity.x + rb.velocity.x))
			{
                signX = 1;
			}
			else
			{
                signX = -1;
			}
            if (Mathf.Abs(currentVelocity.y - rb.velocity.y) < Mathf.Abs(currentVelocity.y + rb.velocity.y))
            {
                signY = 1;
            }
            else
            {
                signY = -1;
            }
            rb.velocity = new Vector2(currentVelocity.x * signX, currentVelocity.y * signY);
        }
    }
}
