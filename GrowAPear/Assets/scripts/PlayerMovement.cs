using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool hasJumped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);


        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;


        if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        hasJumped = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Ground"))
        {
            hasJumped = false;
        }
    }

}