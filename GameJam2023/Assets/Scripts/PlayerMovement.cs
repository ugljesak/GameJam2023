using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	static public float rotationAngle;
	static public float deltaX, deltaY;

	Vector2 orientation;
	Rigidbody2D rb;
	public float moveSpeed;
	public Transform bladeSpawner;
	public Vector3 bladeOffset;
	public float bladeDistance;
	bool right;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		transform.position = Vector3.zero;
		right = true;
	}

	void Update()
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

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(orientation.x * moveSpeed, orientation.y * moveSpeed);

		deltaY = Input.mousePosition.y - transform.position.y - Screen.height / 2;
		deltaX = Input.mousePosition.x - transform.position.x - Screen.width / 2;
		//Debug.DrawLine(Input.mousePosition +camera.transform.position, camera.transform.position, Color.red, 2, false);
	}
}
