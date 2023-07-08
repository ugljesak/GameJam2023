using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerscript : MonoBehaviour
{
    public Vector3 bladeOffset;
    public float bladeDistance;
	[HideInInspector]    
    public Vector2 orientation;
    public GameObject sawBlade;
    public Vector3 playerPosition;

    void Start()
    {

    }

    public void SpawnBlade()
	{
        Quaternion angle = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Atan2(orientation.y, orientation.x)));
        Instantiate(sawBlade, transform.position, angle);
    }

    void FixedUpdate()
    {
        transform.position =  playerPosition + bladeOffset;
        transform.position += (Vector3)(orientation) * bladeDistance;
    }
}
