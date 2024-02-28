using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public float impulsoVoltereta = 5f;
    [SerializeField] LayerMask sueloMask;

    public BoxCollider2D boxCollider2D;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isFacingRight = true;
    private bool puedeRealizarAcciones = true;
    private bool haciendoVoltereta = false;
    private bool defendiendo = false;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(puedeRealizarAcciones){

        if (transform.position.x >= 115.3f)
        {
            // Resta 245 de la posición x
            transform.position = new Vector3(transform.position.x - 213f, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -97.7f)
        {
            // Resta 245 de la posición x
            transform.position = new Vector3(transform.position.x + 213f, transform.position.y, transform.position.z);
        }

            if (haciendoVoltereta)
            {
                // Si está haciendo la voltereta, aplicar un pequeño movimiento lateral
                float movimientoVoltereta = impulsoVoltereta * (isFacingRight ? 1f : -1f);
                rb.velocity = new Vector2(movimientoVoltereta, rb.velocity.y);
            }
            else if (defendiendo){}
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


                if (IsGrounded() && Input.GetKeyDown(KeyCode.K) )
                {
                    animator.SetTrigger("Daño");
                }

                // Salto con la tecla espacio solo si está en el suelo
                if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                // Voltereta con la tecla R solo si está en el suelo
                if (IsGrounded() && Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(IniciarVoltereta());
                }
                if (IsGrounded() && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
                {
                    StartCoroutine(Defender());
                }

            

                if (Input.GetKeyDown(KeyCode.Q) && !IsGrounded())
                {
                    StartCoroutine( AtaqueAire() );
                }else{

                    // Lógica de animación de salto y caída
                    if (!IsGrounded() && rb.velocity.y > 0)
                    {
                        animator.SetBool("Saltar", true);
                        animator.SetBool("Caer", false);
                    }
                    else if (!IsGrounded() && rb.velocity.y < 0)
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
        animator.SetTrigger("Voltereta");

        yield return new WaitForSeconds(0.5f);

        haciendoVoltereta = false;
    }

    public bool IsGrounded()
    {
        RaycastHit2D rayhit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, sueloMask);
        return rayhit.collider != null;
    }

    public IEnumerator Defender()
    {
        animator.SetTrigger("Defensa");
        defendiendo = true;
        yield return new WaitForSeconds(2.5f);
        defendiendo = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si la etiqueta de colisión del objeto que choca es "daño"...
        if (collision.gameObject.tag == "daño")
        {
            // ...activar el trigger "SetTriggerDaño".
            animator.SetTrigger("Daño");
        }
    }
public IEnumerator AtaqueAire()
    {
        puedeRealizarAcciones = false;
        animator.SetBool("Saltar", false);
        animator.SetBool("Caer", false);
        float gravity = rb.gravityScale;
        if (rb.velocity.y > 0){
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
            rb.gravityScale= 0.1f;
        animator.SetTrigger("AirAtk");
        yield return new WaitForSeconds(1f);
        rb.gravityScale = gravity;
        puedeRealizarAcciones = true;
    }


}
