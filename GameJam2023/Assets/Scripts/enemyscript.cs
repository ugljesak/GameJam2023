using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static Unity.Collections.AllocatorManager;
using static UnityEngine.Tilemaps.Tilemap;

public class enemyscript : MonoBehaviour
{
    public PlayerMovement player;
    public List<Vector2> putanja;
    public List<float> inputtime;
    float time;
    public Rigidbody2D rb;
    public float ms;
    public float jurims;
    public float bezims;
    public bool isjuring;
    public Animator animator;
    int i = 0;
    int j = 0;
    bool bilazamena = false;
    float timezainput;
    bool invincible = false;
    int maxhealth = 1;
    public static int health = 1;
    Vector3 pozbezi = new Vector3((float)-8.87, (float)-4.35,1);
    Vector3 pozjuri = new Vector3((float)9.84, (float)4.35,1);

    public Vector2 lookingDirection;
    public GameObject bladeSpawner;
    public Vector3 bladeOffset;
    public float bladeDistance;
    float dashcooldown;
    float sawcooldown;
    public float dashCD;
    public float sawCD;
    public spawnerscript ss;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.05f;
    bool dashujem = false;
    Vector2 dashorientation;
    Vector2 orientation;
    bool canmove = true;
    public float CD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ms = jurims;
        isjuring = true;
        time = 0;
        timezainput = 0;
        transform.position = pozjuri;
        invincible = false;
        health = 1;
        maxhealth = 1;
        CD = 2;
    }

    void FixedUpdate()
    {
        if (bilazamena)
        {
            if (isjuring)
            {
                ms = jurims;
            }
            if (dashujem)
            {
                if (dashcooldown <= dashCD - 0.05)
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
            if (i < putanja.Count)
            {
                orientation = putanja[i];
                i++;
            }
            else
            {
                orientation =new Vector2(0, 0);
            }
            if (orientation != Vector2.zero) lookingDirection = orientation;
            bladeSpawner.GetComponent<spawnerscript>().orientation = lookingDirection;
            if (canmove)
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


                if (j < inputtime.Count)
                {
                    if (time >= inputtime[j])
                    {
                        j++;
                        if (isjuring)
                        {
                            SpawnBlade();
                        }
                        else
                        {
                            Dash();
                        }
                    }
                }
            }
        }
        dashcooldown -= Time.fixedDeltaTime;
        sawcooldown -= Time.fixedDeltaTime;
        CD-=Time.fixedDeltaTime;
        time += Time.fixedDeltaTime;
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
        CD = 2;
        health = maxhealth;
        i = 0;
        j = 0;
        bilazamena = true;
        if (player.GetComponent<PlayerMovement>().putanja != null)
        {
            putanja = new List<Vector2>(player.GetComponent<PlayerMovement>().putanja);
        }
        if (player.GetComponent<PlayerMovement>().inputtime != null)
        {
            inputtime = new List<float>(player.GetComponent<PlayerMovement>().inputtime);
        }
        time = 0;
        if (isjuring)
        {
            ms = bezims;
            isjuring = false;
            invincible = false;
            animator.SetBool("juri", false);
            transform.position = pozbezi;
            maxhealth++;
        }
        else
        {
            ms = jurims;
            isjuring = true;
            invincible = true;
            animator.SetBool("juri", true);
            transform.position = pozjuri;
        }
    }

    private void Dash()
    {
        dashujem = true;
        dashorientation = orientation;
        dashcooldown = dashCD;
        ms *= 10;
        invincible = true;
    }

    private void SpawnBlade()
    {
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
            if (health > 0)
            {
                animator.SetTrigger("hit");
            }
            if (health == 0)
            {
                print("UMRO enemy");
                animator.SetBool("umro",true);
                nutscript.nutCount = 0;
                nutscript.maxNut = maxhealth + 1;
            }
        }
    }

    private void DeathStart()
    {
        canmove = false;
    }

    private void DeathEnd()
    {
        ReverseRoles();
        player.ReverseRoles();
        animator.SetBool("umro", false);
        canmove = true;
        health = maxhealth;
    }

    private void HitEnd()
    {
        animator.SetBool("hit", false);
    }
}
