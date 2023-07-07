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
	public Vector3 gunOffset;
	public Vector3 bulletSpawnerOffset;
	public float delayFactor;
	bool right;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		transform.position = Vector3.zero;
		gunOffset = new Vector3(0.1f, 0.1f, -2f);
		bulletSpawnerOffset = new Vector3(0.6f, 0.09f, -4f);
		right = true;
	}

	void Update()
	{
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");
		orientation = new Vector2(moveX, moveY).normalized;

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
