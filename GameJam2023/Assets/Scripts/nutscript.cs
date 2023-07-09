using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutscript : MonoBehaviour
{
	public static int nutCount;
	public static int maxNut;

    void Start()
    {
		maxNut = 10;
		nutCount = 0;
    }

    private void FixedUpdate()
    {
		if (PlayerMovement.isjuring == true)
		{
            transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Wall")
		{
			transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 1);
			return;
		}
		print("UVACEN SI BATO BRE");
		if(collision.gameObject.tag == "Player")
		{
			gameObject.GetComponent<AudioSource>().Play();
			nutCount++;
			if (maxNut > nutCount)
			{
				transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 1);
				
			}
			else transform.position = new Vector3(100.0f, 100.0f, 0.0f);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 1);
			return;
		}
	}
}
