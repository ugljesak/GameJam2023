using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class nutscript : MonoBehaviour
{
	public static int nutCount = 0;
	public static int maxNut;
	// maxNut i nutCount se menjaju u PlayerMovement.ReverseRoles()
    void Start()
    {
		maxNut = 1;
		nutCount = 0;
        transform.position = new Vector3(Random.Range(0, 10), Random.Range(-5, 5), 1);
    }

    private void FixedUpdate()
    {
		if (PlayerMovement.isjuring == true)
		{
            transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
