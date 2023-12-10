using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private float meleeRange = 2f;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Animator animator;
	[SerializeField] private PlayerSFXScript playerSFXScript;

	private bool isGrounded = true;
	private bool isAttacking = false;
	private bool isDead = false;

	[SerializeField] private GameObject attackCollider;

	public bool IsDead { get { return isDead; } set { isDead = value; } }

	private void Start()
	{
		attackCollider?.SetActive(false);
	}

	void Update()
    {
		if (isDead || isAttacking) return;

		Debug.DrawLine(transform.position, transform.position + transform.forward * meleeRange);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (isGrounded)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(Attack());
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
			transform.rotation = Quaternion.Euler(new Vector3(0f, moveVelocity > 0f ? 90f : -90f, 0f));
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

	private IEnumerator Attack()
	{
		isAttacking = true;
		animator.Play("AttackAnim");
		playerSFXScript.PlayerHitSFX();
		attackCollider.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		attackCollider.SetActive(false);
		isAttacking = false;
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