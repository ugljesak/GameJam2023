using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	static public float rotationAngle;
	static public float deltaX, deltaY;
	public float time;
	Vector2 orientation;
	Rigidbody2D rb;
	float ms; // move speed
	public float jurims;
	public float bezims;
	public Transform bladeSpawner;
	public Vector3 bladeOffset;
	public float bladeDistance;
	private float dashcooldown;
	bool right;
	bool canmove = true;
	bool isjuring = true;
	List<Vector2> putanja= new List<Vector2>();
	List<float> inputtime= new List<float>();

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		transform.position = Vector3.zero;
		right = true;
		ms = jurims;
		time = 0;
		dashcooldown = 0;
	}

	void Update()
	{
		if (canmove)
		{
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");
			orientation = new Vector2(moveX, moveY).normalized;

			bladeSpawner.position = transform.position + bladeOffset + new Vector3(orientation.x * bladeDistance, orientation.y * bladeDistance, 0);

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
	}

	private void FixedUpdate()
	{
		putanja.Add(transform.position);
		if (canmove)
		{
			rb.velocity = new Vector2(orientation.x * ms, orientation.y * ms);

			deltaY = Input.mousePosition.y - transform.position.y - Screen.height / 2;
			deltaX = Input.mousePosition.x - transform.position.x - Screen.width / 2;
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
					if(dashcooldown<=0) Dash();
				}
			}
		}
		print (orientation);
		dashcooldown-= Time.fixedDeltaTime;
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
		transform.position += (Vector3)orientation;
		dashcooldown = 3;
	}
}
