using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float impulsoVoltereta = 5f;
    [SerializeField] LayerMask sueloMask;
    
    public BoxCollider2D boxCollider2D;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isFacingRight = true;
    private bool puedeRealizarAcciones = true;
    private bool haciendoVoltereta = false;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Verificar si está en el suelo

        if (haciendoVoltereta)
        {
            // Si está haciendo la voltereta, aplicar un pequeño movimiento lateral
            float movimientoVoltereta = impulsoVoltereta * (isFacingRight ? 1f : -1f);
            rb.velocity = new Vector2(movimientoVoltereta, rb.velocity.y);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // Movimiento lateral y lógica de voltear solo si está en el suelo
            
                // Movimiento lateral normal
                Vector2 movement = new Vector2(horizontalInput, 0f);
                rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

                // Lógica de voltear
                if (horizontalInput > 0 && !isFacingRight)
                {
                    Flip();
                    animator.SetBool("Andar", true);
                }
                else if (horizontalInput < 0 && isFacingRight)
                {
                    Flip();
                    animator.SetBool("Andar", true);
                }
                else
                {
                    animator.SetBool("Andar", false);
                }
                // Salto con la tecla espacio solo si está en el suelo
                if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

            // Voltereta con la tecla R solo si está en el suelo
            if (IsGrounded() && Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(IniciarVoltereta());
            }

            // Lógica de animación de salto y caída
            if (rb.velocity.y > 0)
            {
                animator.SetBool("Saltar", true);
                animator.SetBool("Caer", false);
            }
            else if (rb.velocity.y < 0)
            {
                animator.SetBool("Saltar", false);
                animator.SetBool("Caer", true);
            }
            else
            {
                animator.SetBool("Saltar", false);
                animator.SetBool("Caer", false);
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator IniciarVoltereta()
    {
        // Reiniciar impulso previo
        rb.velocity = new Vector2(0f, rb.velocity.y);

        haciendoVoltereta = true;
        animator.SetBool("Voltereta", true);

        yield return new WaitForSeconds(0.7f);

        FinalizarVoltereta();
    }

    // Método llamado por la animación al finalizar la voltereta
    public void FinalizarVoltereta()
    {
        animator.SetBool("Voltereta", false);
        haciendoVoltereta = false;
    }

    public bool IsGrounded(){
        RaycastHit2D rayhit= Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, sueloMask);
        return rayhit.collider!=null;
    }

}
