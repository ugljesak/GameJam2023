using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static bool isjuring = true;
	public static bool canmove = true;
	
	float time;
	[HideInInspector]
	public float deltaX, deltaY;
	Vector2 orientation;
	public Vector2 lookingDirection;
	Rigidbody2D rb;
	float ms; // move speed
	public float jurims;
	public float bezims;
	public GameObject bladeSpawner;
	public Vector3 bladeOffset;
	public float bladeDistance;
	public float dashcooldown;
	bool right;
	public List<Vector2> putanja= new List<Vector2>();
	public List<float> inputtime= new List<float>();
	public int brsrafova;
	public spawnerscript ss;
	public Animator animator;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		transform.position = Vector3.zero;
		right = true;
		ms = jurims;
		time = 0;
		dashcooldown = 0;
		putanja.Add(Vector2.zero);
	}

	void Update()
	{
		if (canmove)
		{
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");
			animator.SetFloat("xAxis", moveX);
			animator.SetFloat("yAxis", moveY);
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
	}

	private void FixedUpdate()
	{
		putanja.Add(orientation);
		if (canmove)
		{
			rb.velocity = new Vector2(orientation.x * ms, orientation.y * ms);
			bladeSpawner.GetComponent<spawnerscript>().playerPosition = transform.position;

			deltaY = Input.mousePosition.y - transform.position.y - Screen.height / 2;
			deltaX = Input.mousePosition.x - transform.position.x - Screen.width / 2;
			//Debug.DrawLine(Input.mousePosition +camera.transform.position, camera.transform.position, Color.red, 2, false);

			if (Input.GetKeyUp(KeyCode.Space))
			{
				if (isjuring)
				{
					animator.SetBool("", true);
					ss.SpawnBlade();
				}
				else
				{
					if (dashcooldown <= 0)
					{
						Dash();
					}
				}
			}
		}
		//print (orientation);
		dashcooldown-= Time.fixedDeltaTime;
		//print(dashcooldown);
		time += Time.fixedDeltaTime;
		if (time >= 5)
		{
			print("ROLES REVERSED");
			ReverseRoles();
		}
	}

	private void ReverseRoles()
	{

		putanja.Clear();
		inputtime.Clear();
		time = 0;
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
		inputtime.Add(time);
		transform.position += (Vector3)(lookingDirection.normalized);
		dashcooldown = 2;
	}
}
