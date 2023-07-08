using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	float time;
	public float deltaX, deltaY;
	Vector2 orientation;
	public Vector2 lookingDirection;
	Rigidbody2D rb;
	float ms; // move speed
	public float dashCooldown;
	public float jurims;
	public float bezims;
	public GameObject bladeSpawner;
	bool right;
	bool canmove = true;
	bool isjuring = true;
	List<Vector2> putanja= new List<Vector2>();
	List<float> inputtime= new List<float>();

	void Start()
	{
		//Instantiate(bladeSpawner, transform.position, Quaternion.identity);
		rb = GetComponent<Rigidbody2D>();
		transform.position = Vector3.zero;
		right = true;
		ms = jurims;
		time = 0;
		dashCooldown = 0;
	}

	void Update()
	{
		if (!canmove)
		{
			return;
		}

		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");
		orientation = new Vector2(moveX, moveY).normalized;
		if (orientation != Vector2.zero) lookingDirection = new Vector2(moveX, moveY);
		bladeSpawner.GetComponent<spawnerscript>().orientation = lookingDirection;

		if (moveX < 0) right = false;
		if (moveX > 0) right = true;
		if (right == false)
		{
			transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
		}
		else
		{
			transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
		}
	}

	private void FixedUpdate()
	{
		if (!canmove)
		{
			return;
		}
		rb.velocity = new Vector2(orientation.x * ms, orientation.y * ms);
		bladeSpawner.GetComponent<spawnerscript>().playerPosition = transform.position;

		putanja.Add(transform.position);
		//Debug.DrawLine(Input.mousePosition +camera.transform.position, camera.transform.position, Color.red, 2, false);

		if (Input.GetKey(KeyCode.Space))
		{
			inputtime.Add(time);
			if (isjuring)
			{
				//baci seckalicu
			}
			else
			{
				if(dashCooldown<=0) Dash();
			}
		}
		dashCooldown-= Time.fixedDeltaTime;
		//print(dashcooldown);
        time += Time.fixedDeltaTime;
		if (time >= 5)
		{
			print("ROLES REVERSED");
			ReverseRoles();
			time = 0;
		}
	}

    private void ReverseRoles()
    {
		if (isjuring)
		{
			ms = bezims;
			isjuring = false;
		}
		else
		{
			ms = jurims;
			isjuring = true;
		}
    }

	private void Dash()
	{
		transform.position += (Vector3)(lookingDirection.normalized);
		dashCooldown = 2;
	}
}
