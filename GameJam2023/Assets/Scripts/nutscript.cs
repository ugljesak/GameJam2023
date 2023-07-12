using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class nutscript : MonoBehaviour
{
	public static int nutCount = 0;
	public static int maxNut;
	// maxNut i nutCount se menjaju u PlayerMovement.ReverseRoles()
	public static float maxtime;
	public static float timer;
    void Start()
    {
		maxNut = 1;
		nutCount = 0;
        transform.position = new Vector3(Random.Range(0, 10), Random.Range(-5, 5), 1);
		maxtime = 10;
		timer = 10;
    }

    private void FixedUpdate()
    {
		if (timer <= 0 && !PlayerMovement.isjuring)
		{
			PlayerMovement.health--;
		}
		timer = Mathf.Max(timer - Time.fixedDeltaTime, 0); 
        if (PlayerMovement.isjuring == true)
		{
            transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            RandomPosition();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		print("UVACEN SI BATO BRE");
		if(collision.gameObject.tag == "Player")
		{
			gameObject.GetComponent<AudioSource>().Play();
			nutCount++;
			if (maxNut > nutCount)
			{
				RandomPosition();
				
			}
			else transform.position = new Vector3(100.0f, 100.0f, 0.0f);
		}
	}

	public void RandomPosition()
	{
		if (nutCount % 2 == 0)
		{
            transform.position = new Vector3(Random.Range(0, 10), Random.Range(-5, 5), 1);
        }
		else
		{
            transform.position = new Vector3(Random.Range(-10, 0), Random.Range(-5, 5), 1);
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "block")
		{
			transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 1);
			return;
		}
	}
}
