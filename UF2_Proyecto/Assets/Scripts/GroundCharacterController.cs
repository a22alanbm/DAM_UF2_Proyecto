using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private bool isFacingRight = true;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

// Check for ground contact (adjust the layer mask as needed)
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Ground"));

        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Flip the character
        if (horizontalInput > 0 && !isFacingRight)
        {
            animator.SetBool("Correr", true);
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            animator.SetBool("Correr", true);
            Flip();
        }
        else{
            animator.SetBool("Correr", false);
        }

        // Jumping
        if (isGrounded && Input.GetKeyDown("Space"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }



    }
    private void Flip()
    {
        // Flip the character by toggling the sprite renderer's flipX property
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}