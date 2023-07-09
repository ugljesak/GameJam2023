using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutscript : MonoBehaviour
{
	static int nutCount; 

    void Start()
    {
        
    }
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		print("UVACEN SI BATO BRE");
		if(collision.gameObject.tag == "Player")
		{
			nutCount++;
			transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 1);
		}
	}
}
