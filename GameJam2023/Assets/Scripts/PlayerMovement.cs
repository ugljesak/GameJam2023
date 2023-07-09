using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static bool isjuring;
	public static bool canmove = true;
	public static int health = 1;
	public static int score = 0;

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
	float dashcooldown;
	float sawcooldown;
	public float dashCD;
	public float sawCD;
	public List<Vector2> putanja= new List<Vector2>();
	public List<float> inputtime= new List<float>();
	public int brsrafova;
	public spawnerscript ss;
	public Animator animator;
	public ContactFilter2D movementFilter;
	List<RaycastHit2D> castCollisions= new List<RaycastHit2D>();
	public float collisionOffset = 0.05f;
	bool dashujem = false;
	Vector2 dashorientation;
	bool invincible = true;
	Vector2 pozbezi=new Vector2(-1,-1);
    Vector2 pozjuri = new Vector2(1, 1);
	public float CD;
	bool canmovecd = false;



    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		transform.position = pozbezi;
		ms = jurims;
		time = 0;
		dashcooldown = 0;
		sawcooldown = 0;
		putanja.Add(Vector2.zero);
		score = 0;
		invincible = true;
		health = 10;
		CD = 2;
		isjuring = false;
	}

	void Update()
	{
		if (canmove && canmovecd)
		{
			float moveX = Input.GetAxisRaw("Horizontal");
			float moveY = Input.GetAxisRaw("Vertical");
			orientation = new Vector2(moveX, moveY).normalized;
			if (orientation != Vector2.zero) lookingDirection = new Vector2(moveX, moveY).normalized;
			bladeSpawner.GetComponent<spawnerscript>().orientation = lookingDirection;
		}
	}

	private void FixedUpdate()
	{
		
		if (isjuring)
		{
			ms = jurims;
		}
		if (dashujem)
		{
			if (dashcooldown <= dashCD-0.05)
			{
				dashujem = false;
				ms /= 10;
				invincible = false;	
			}
			orientation = dashorientation;
		}
		else if (isjuring)
        {
            ms = jurims;
        }
		else
		{
			ms = bezims;
		}
        putanja.Add(orientation);
		
        if (canmove && canmovecd)
		{
            if (orientation.x > 0)
            {
                animator.SetBool("isright", true);
                animator.SetBool("isleft", false);
            }
            else if (orientation.x == 0)
            {
                animator.SetBool("isright", false);
                animator.SetBool("isleft", false);
            }
            else
            {
                animator.SetBool("isleft", true);
                animator.SetBool("isright", false);
            }

            if (orientation.y > 0)
            {
                animator.SetBool("isup", true);
                animator.SetBool("isdown", false);
            }
            else if (orientation.y == 0)
            {
                animator.SetBool("isup", false);
                animator.SetBool("isdown", false);
            }
            else
            {
                animator.SetBool("isdown", true);
                animator.SetBool("isup", false);
            }
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
                animator.SetBool("ismoving", true);
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
		CD -= Time.fixedDeltaTime;
		//print(dashcooldown);
		time += Time.fixedDeltaTime;
		if (CD <= 0)
		{
			canmovecd = true;
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

	public void ReverseRoles()
	{
		rolesmovement.revv = true;
		putanja.Clear();
		inputtime.Clear();
        time = 0;
        CD = 2;
        canmovecd = false;
        if (isjuring)
		{
			ms = bezims;
			isjuring = false;
			invincible = false;
			animator.SetBool("juri", false);
			dashcooldown = dashCD;
			transform.position = pozbezi;
			health = 1;
		}
		else
		{
			ms = jurims;
			isjuring = true;
			invincible = true;
            animator.SetBool("juri", true);
			sawcooldown = sawCD;
			transform.position = pozjuri;
			score++;
        }
	}

	private void Dash()
	{
		inputtime.Add(time);
		dashujem = true;
		dashorientation = orientation;
		dashcooldown = dashCD;
		ms *= 10;
		invincible = true;
	}

	private void SpawnBlade()
	{
		inputtime.Add(time);
		if (lookingDirection.x == 0)
		{
			if (lookingDirection.y > 0)
			{
				animator.SetTrigger("sawu");
			}
			else
			{
				animator.SetTrigger("sawd");
			}
		}
		else if (lookingDirection.x > 0)
		{
			if (lookingDirection.y > 0)
			{
				animator.SetTrigger("sawur");
			}
			else if (lookingDirection.y < 0)
			{
                animator.SetTrigger("sawdr");
            }
			else
			{
                animator.SetTrigger("sawr");
            }
		}
		else
		{
            if (lookingDirection.y > 0)
            {
                animator.SetTrigger("sawul");
            }
            else if (lookingDirection.y < 0)
            {
                animator.SetTrigger("sawdl");
            }
            else
            {
                animator.SetTrigger("sawl");
            }
        }
    }

	private void SawStart()
	{
		canmove = false;
        animator.SetBool("ismoving", false);
		animator.SetBool("sawblade", true);
        sawcooldown = sawCD;
		gameObject.GetComponent<AudioSource>().Play();
    }

	private void SawEnd()
	{
        ss.SpawnBlade();
        canmove = true;
		animator.SetBool("sawblade", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "saw" && !invincible)
		{
			health--;
			if (health == 0)
			{
				animator.SetBool("umro",true);
				print("UMRO player");
			}
		}
	}

	private void DeathStart()
	{
		canmove = false;
	}

	private void DeathEnd()
	{
		Destroy(gameObject);
	}
}
