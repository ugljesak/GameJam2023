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
	//public List<Vector2> positions;
    public List<Vector2> lastpositions = new List<Vector2>();
	public int i = 1;
	public Animator animator;

    void Start()
    {
		maxNut = 1;
		nutCount = 0;
        RandomPosition();
		maxtime = 10;
		timer = 10;
    }

    private void FixedUpdate()
    {
		if (timer <= 0 && !PlayerMovement.isjuring)
		{
			PlayerMovement.health--;
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player" && !PlayerMovement.isjuring)
		{
			gameObject.GetComponent<AudioSource>().Play();
			nutCount++;
            lastpositions.Add(transform.position);
            if (maxNut > nutCount)
			{
				RandomPosition();		
			}
			else transform.position = new Vector3(100.0f, 100.0f, 0.0f);
		}
		if (collision.gameObject.tag == "enemy" && PlayerMovement.isjuring)
		{
			nutCount++;
			if (maxNut > nutCount)
			{
				transform.position = lastpositions[i];
				i++;
			}
			else transform.position=new Vector3(100.0f, 100.0f, 0.0f);
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
		if (Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) <= 3)
		{
			RandomPosition();
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
