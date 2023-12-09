using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Animator animator;

	private bool isGrounded = true;

	private void Start()
	{
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isGrounded)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}

		var moveVelocity = 0f;

		//Left Right Movement
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			moveVelocity = -moveSpeed;
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			moveVelocity = moveSpeed;
		}

		if (moveVelocity != 0f)
		{
			transform.rotation = Quaternion.Euler(new Vector3(0f, moveVelocity > 0f ? 135f : -135f, 0f));
		}
		
		rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

		if (isGrounded)
		{
			animator.Play(moveVelocity == 0f ? "IdleAnim" : "MoveAnim");
		}
		else
		{
			animator.Play("JumpAnim");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag("Platform"))
		{
			isGrounded = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.transform.CompareTag("Platform"))
		{
			isGrounded = false;
		}
	}
}