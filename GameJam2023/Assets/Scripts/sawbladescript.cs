using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class sawbladescript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    float time;
    public float lifeTime;
    public static bool revv = false;
    Vector2 pravaczida;
    Vector2 pravacnormale;
    Vector2 pravacparalele;

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

	private void Update()
	{
        //print(revv);
		if(revv == true && PlayerMovement.zavrsio == false)
		{
            Destroy(gameObject);
		}
	}


    private void OnTriggerEnter2D(Collider2D collision)
	{
        if(collision.gameObject.tag == "Wall")
		{
            gameObject.GetComponent<AudioSource>().Play();
            pravaczida = collision.gameObject.GetComponent<wallscript>().pravac;
            pravacnormale.x = -pravaczida.y;
            pravacnormale.y = pravaczida.x;
            rb.velocity = Vector3.Project(rb.velocity, pravaczida) - Vector3.Project(rb.velocity, pravacnormale);
        }
        if (collision.gameObject.tag == "saw")
        {
            pravacnormale = gameObject.transform.position - collision.transform.position;
            pravacparalele.x = -pravacnormale.y;
            pravacparalele.y=pravacnormale.x;
            rb.velocity = Vector3.Project(rb.velocity, pravacparalele) - Vector3.Project(rb.velocity, pravacnormale);
        }
    }
}
