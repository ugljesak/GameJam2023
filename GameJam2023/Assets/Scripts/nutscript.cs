using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutscript : MonoBehaviour
{
	static int nutCount; 

    void Start()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		print("UVACEN SI BATO BRE");
		if(collision.gameObject.tag == "Player")
		{
			nutCount++;
			Destroy(gameObject);
			Instantiate(gameObject, Vector3.zero, Quaternion.identity);
		}
	}
}
