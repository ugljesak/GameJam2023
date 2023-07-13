using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
	public static bool isjuring;
	public static bool canmove = true;
	public static int health = 1;
	public static int score = 0;
	static public bool zavrsio = false;
	static public int highscore2;

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
	bool invincible = false;
	Vector2 pozbezi=new Vector2((float)-8.87, (float)-4.35);
    Vector2 pozjuri = new Vector2((float)9.84, (float)4.35);
	float CD;
	public float CDbetweenrounds;
	public GameObject reverseSound;
	public GameObject deathSound;
	public GameObject dashSound;
	bool isattacking = false;
	string curanim;
	bool isdying = false;
	public nutscript nut;
	public finishscript finish;
	Scene currentscene;

    void Start()
	{
        currentscene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		transform.position = pozbezi;
		ms = jurims;
		time = 0;
		dashcooldown = 0;
		sawcooldown = 0;
		putanja.Add(Vector2.zero);
		score = 0;
		invincible = false;
		health = 1;
		isjuring = false;
		canmove = false;
		curanim = "playeridle1";
		CD = CDbetweenrounds;
	}

	void ChangeAnimation(string newanim)
	{
		if (!CanChangeAnimation(newanim)) return;
		animator.Play(newanim);
		curanim = newanim;
	}

	bool CanChangeAnimation(string newanim)
	{
        if (newanim == curanim) return false;
		if (isattacking) return false;
		if(isdying) return false;
		return true;
    }

	void Update()
	{
		if (canmove)
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
        if (health <= 0)
        {
			ChangeAnimation("playerdeath");
			enemyscript.invincible = true;
			if (currentscene.name == "Map2")
			{
				if (nutscript.maxNut > highscore2) 
				{
					highscore2 = nutscript.maxNut;
					PlayerPrefs.SetInt("highscore2",highscore2);
				}
			}
			return;
        }
        if (isjuring)
		{
			ms = jurims;
		}
		if (dashujem && !isjuring)
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
		
        if (canmove)
		{
			if (orientation.x > 0)
			{
				if (isjuring) ChangeAnimation("playerrunright");
				else ChangeAnimation("playerrunright1");
			}
            else if(orientation.x<0)
            {
                if (isjuring) ChangeAnimation("playerrunleft");    
                else ChangeAnimation("playerrunleft1");
            }
			else
			{
				if (orientation.y > 0)
				{
                    if (isjuring) ChangeAnimation("playerrunup");
                    else ChangeAnimation("playerrunup1");
                }
				else if(orientation.y<0)
				{
                    if (isjuring) ChangeAnimation("playerrundown");
                    else ChangeAnimation("playerrundown1");
                }
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
            }
            else
            {
				if (isjuring) ChangeAnimation("playeridle");
				else ChangeAnimation("playeridle1");
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
		else if(!isattacking)
		{
			if (isjuring) ChangeAnimation("playeridle"); 
			else ChangeAnimation("playeridle1");
		}
		//print (orientation);
		dashcooldown-= Time.fixedDeltaTime;
		sawcooldown-= Time.fixedDeltaTime;
		CD -= Time.fixedDeltaTime;
		if(CD <= 1.5f)
		{
			sawbladescript.revv = false;
		}
		//print(dashcooldown);
		time += Time.fixedDeltaTime;
		if (CD <= 0)
		{
            nutscript.timer = Mathf.Max(nutscript.timer - Time.fixedDeltaTime, 0);
        }
		if (CD <= 0 && CD>=-0.02) //|| zavrsio == true)
		{
			rolesmovement.revv = false;
			reversedmovement.revv = false;
			sawbladescript.revv = false;
			zavrsio = false;
			canmove = true;
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
		print("ROLES REVERSED");
		reverseSound.GetComponent<AudioSource>().Play();
		putanja.Clear();
		inputtime.Clear();
        time = 0;
        CD = CDbetweenrounds;
		rolesmovement.revv = true;
		reversedmovement.revv = true;
		sawbladescript.revv = true;
        nutscript.nutCount = 0;
        finish.animator.Play("empty");
        finish.dosoportal = false;
        if (isjuring)
        {
            invincible = false;
            ms = bezims;
			isjuring = false;
			dashcooldown = dashCD;
			transform.position = pozbezi;
			health = 1;
            animator.Play("playeridle1");
			nut.RandomPosition();
            nutscript.maxNut++;
            nutscript.maxtime += 2;
			nutscript.timer = nutscript.maxtime;
			nut.lastpositions.Clear();
			nut.animator.Play("nut");
        }
		else
        {
            invincible = true;
            ms = jurims;
			isjuring = true;
			sawcooldown = sawCD;
			transform.position = pozjuri;
			score++;
            animator.Play("playeridle");
			nut.transform.position = nut.lastpositions[0];
			nut.i = 1;
            nut.animator.Play("nutopacity");

        }
		canmove = false;
		isattacking = false;
	}

	private void Dash()
	{
		dashSound.GetComponent<AudioSource>().Play();
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
				ChangeAnimation("playerblehup");
			}
			else
			{
                ChangeAnimation("playerblehdown");
            }
		}
		else if (lookingDirection.x > 0)
		{
			if (lookingDirection.y > 0)
			{
                ChangeAnimation("playerblehur");
            }
			else if (lookingDirection.y < 0)
			{
                ChangeAnimation("playerblehdr");
            }
			else
			{
                ChangeAnimation("playerblehright");
            }
		}
		else
		{
            if (lookingDirection.y > 0)
            {
                ChangeAnimation("playerblehul");
            }
            else if (lookingDirection.y < 0)
            {
                ChangeAnimation("playerblehdl");
            }
            else
            {
                ChangeAnimation("playerblehleft");
            }
        }
    }

	private void SawStart()
	{
		canmove = false;
        sawcooldown = sawCD;
		isattacking = true;
		gameObject.GetComponent<AudioSource>().Play();
    }

	private void SawEnd()
	{
        ss.SpawnBlade();
        canmove = true;
		isattacking = false;
        ChangeAnimation("playeridle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "saw" && !invincible && health>0)
		{
			print(invincible);
			print("aaaaaaaaaaa");
			health--;
            deathSound.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
		}
	}

	private void DeathStart()
	{
		canmove = false;
		isdying = true;
	}

	private void DeathEnd()
	{
		isdying = false;
		canmove = true;
	}
}
