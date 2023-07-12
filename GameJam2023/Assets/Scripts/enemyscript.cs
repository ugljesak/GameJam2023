using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build;
//using UnityEditor.Experimental.GraphView;
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
    public static int maxhealth = 1;
    public static int health = 1;
    Vector3 pozbezi = new Vector3((float)-8.87, (float)-4.35,1);
    Vector3 pozjuri = new Vector3((float)9.84, (float)4.35,1);
    bool isattacking = false;
    string curanim;

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
    bool ishit = false;
    bool isdying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ms = jurims;
        isjuring = true;
        time = 0;
        transform.position = pozjuri;
        invincible = false;
        health = 1;
        maxhealth = 1;
        CD = 2;
        curanim = "enemyidle1";
    }

    void ChangeAnimation(string newanim)
    {
        if (!CanChangeAnimation(newanim)) return;
        animator.Play(newanim);
        curanim = newanim;
    }

    bool CanChangeAnimation(string newanim)
    {
        if (isattacking) return false;
        if(isdying) return false;
        if (curanim == "enemyhit" && newanim=="enemyhit") return true;
        if (ishit) return false;
        if (curanim == newanim) return false;
        return true;
    }

    void FixedUpdate()
    {
        if (bilazamena)
        {
            if (health <= 0)
            {
                ChangeAnimation("enemydeath");
            }
            if (isjuring)
            {
                ms = jurims;
            }
            if (dashujem && !isjuring)
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
                    if (isjuring) ChangeAnimation("enemyrunright");
                    else ChangeAnimation("enemyrunright1");
                }
                else if (orientation.x < 0)
                {
                    if (isjuring) ChangeAnimation("enemyrunleft");
                    else ChangeAnimation("enemyrunleft1");
                }
                else
                {
                    if (orientation.y > 0)
                    {
                        if (isjuring) ChangeAnimation("enemyrunup");
                        else ChangeAnimation("enemyrunup1");
                    }
                    else if (orientation.y < 0)
                    {
                        if (isjuring) ChangeAnimation("enemyrundown");
                        else ChangeAnimation("enemyrundown1");
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
                    if (isjuring) ChangeAnimation("enemyidle");
                    else ChangeAnimation("enemyidle1");
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
            else if(!isattacking)
            {
                if (isjuring) ChangeAnimation("enemyidle");
                else ChangeAnimation("enemyidle1");
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
            transform.position = pozbezi;
            maxhealth++;
            animator.Play("enemyidle1");
        }
        else
        {
            ms = jurims;
            isjuring = true;
            invincible = true;
            transform.position = pozjuri;
            animator.Play("enemyidle");
        }
        canmove = true;
        isattacking = false;
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
                ChangeAnimation("enemyblehup");
            }
            else
            {
                ChangeAnimation("enemyblehdown");
            }
        }
        else if (lookingDirection.x > 0)
        {
            if (lookingDirection.y > 0)
            {
                ChangeAnimation("enemyblehur");
            }
            else if (lookingDirection.y < 0)
            {
                ChangeAnimation("enemyblehdr");
            }
            else
            {
                ChangeAnimation("enemyblehright");
            }
        }
        else
        {
            if (lookingDirection.y > 0)
            {
                ChangeAnimation("enemyblehul");
            }
            else if (lookingDirection.y < 0)
            {
                ChangeAnimation("enemyblehdl");
            }
            else
            {
                ChangeAnimation("enemyblehleft");
            }
        }
    }

    private void SawStart()
    {
        canmove = false;
        sawcooldown = sawCD;
        isattacking = true;
    }

    private void SawEnd()
    {
        ss.SpawnBlade();
        isattacking = false;
        ChangeAnimation("enemyidle");
        canmove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "saw" && !invincible)
        {
            health--;
            if (health > 0)
            {
                ChangeAnimation("enemyhit");
            }
            if (health <= 0)
            {
                print("UMRO enemy");
            }
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
        ReverseRoles();
        player.ReverseRoles();
        ChangeAnimation("enemyidle1");
        canmove = true;
        health = maxhealth;
    }

    private void HitStart()
    {
        ishit = true;
    }
    private void HitEnd()
    {
        ishit = false;
        ChangeAnimation("enemyidle1");
    }
}
