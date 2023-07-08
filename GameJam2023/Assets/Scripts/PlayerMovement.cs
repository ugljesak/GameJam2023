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
	public Rigidbody2D rb; 
	public float ms;
	public float jurims;
	public float bezims;
	public GameObject bladeSpawner;
	public Vector3 bladeOffset;
	public float bladeDistance;
	public float dashcooldown;
	public float sawcooldown;
	public List<Vector2> putanja= new List<Vector2>();
	public List<float> inputtime= new List<float>();
	public int brsrafova;
	public spawnerscript ss;
	public Animator animator;
	public ContactFilter2D movementFilter;
	List<RaycastHit2D> castCollisions= new List<RaycastHit2D>();
	public float collisionOffset = 0.05f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		transform.position = Vector3.zero;
		ms = jurims;
		time = 0;
		dashcooldown = 0;
		sawcooldown = 0;
		putanja.Add(Vector2.zero);
	}

	void Update()
	{
		if (canmove)
		{
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");
			orientation = new Vector2(moveX, moveY).normalized;
			if (orientation != Vector2.zero) lookingDirection = new Vector2(moveX, moveY);
			bladeSpawner.GetComponent<spawnerscript>().orientation = lookingDirection;
		}
	}

	private void FixedUpdate()
	{
		putanja.Add(orientation);
		if (canmove)
		{
            if (orientation != Vector2.zero)
            {
                bool success = TryMove(orientation);
                if (!success && orientation.x != 0)
                {
                    success = TryMove(new Vector2(orientation.x, 0));

                }
                if (!success && orientation.y != 0)
                {
                    success = TryMove(new Vector2(0, orientation.y));
                }
                animator.SetBool("ismoving", success);
            }
            else
            {
                animator.SetBool("ismoving", false);
            }

			bladeSpawner.GetComponent<spawnerscript>().playerPosition = transform.position;
			//Debug.DrawLine(Input.mousePosition +camera.transform.position, camera.transform.position, Color.red, 2, false);

			if (Input.GetKey(KeyCode.Space))
			{
				if (isjuring)
				{

					if (sawcooldown <= 0)
					{
						SpawnBlade();
					}
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
		sawcooldown-= Time.fixedDeltaTime;
		//print(dashcooldown);
		time += Time.fixedDeltaTime;
		if (time >= 5)
		{
			print("ROLES REVERSED");
			ReverseRoles();
		}
	}

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero) return false;
        int count = rb.Cast(direction, movementFilter, castCollisions, ms * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * ms * Time.fixedDeltaTime);
            return true;
        }
        return false;
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
		print("dash");
		inputtime.Add(time);
		transform.position += (Vector3)(lookingDirection.normalized);
		dashcooldown = 2;
	}

	private void SpawnBlade()
	{
		print("blade");
        ss.SpawnBlade();
		sawcooldown = 2;
    }
}
