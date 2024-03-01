using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 13f;
    private float impulsoVoltereta = 5f;
    public LayerMask sueloMask;

    private BoxCollider2D boxCollider2D;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isFacingRight = true;
    private bool puedeRealizarAcciones = true;
    private bool haciendoVoltereta = false;
    private bool defendiendo = false;
    private bool atacando = false;

    protected float TimeAtk1;
    protected float TimeAtk2;
    protected float TimeAtk3;
    protected float TimeSpecialAtk;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        TimeInitializer();
    }

    void Update()
    {
        if (puedeRealizarAcciones)
        {
            FueraDelMapa();

            if (haciendoVoltereta)
            {
                // Si está haciendo la voltereta, aplicar un pequeño movimiento lateral
                float movimientoVoltereta = impulsoVoltereta * (isFacingRight ? 1f : -1f);
                rb.velocity = new Vector2(movimientoVoltereta, rb.velocity.y);
            }
            else if (defendiendo) { }
            else if (atacando) { }
            else
            {

                float horizontalInput = Input.GetAxis("Horizontal");
                // Movimiento lateral normal
                MovimientoLateral();
                // Lógica de voltear
                Voltear();
                if (IsGrounded() && Input.GetKeyDown(KeyCode.K))
                {
                    recibirDaño(0);
                }
                // Salto con la tecla espacio solo si está en el suelo
                Saltar();
                // Voltereta con la tecla R solo si está en el suelo
                StartCoroutine(Voltereta());
                //Defender con la tecla S solo si está en el suelo
                StartCoroutine(Defender());
                StartCoroutine(AtkPrimario());
                StartCoroutine(AtkEspecial());
                //Si hace el ataque en el aire no puede ejecutarse el codigo 
                //enElAire, pues se sobreescribe y se pierde la amación
                if (Input.GetKeyDown(KeyCode.Q) && !IsGrounded())
                {
                    StartCoroutine(AtaqueAire());
                }
                else
                {
                    // Lógica de animación de salto y caída
                    enElAire();
                }



            }
        }
    }

    private void MovimientoLateral()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }



    private void FueraDelMapa()
    {
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
    }

    private void Voltear()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator Voltereta()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.R))
        {
            // Reiniciar impulso previo
            rb.velocity = new Vector2(0f, rb.velocity.y);

            haciendoVoltereta = true;
            animator.SetTrigger("Voltereta");

            yield return new WaitForSeconds(1f);

            haciendoVoltereta = false;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D rayhit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, sueloMask);
        return rayhit.collider != null;
    }

    private void enElAire()
    {
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

    private void Saltar()
    {
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public IEnumerator Defender()
    {
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            animator.SetTrigger("Defensa");
            defendiendo = true;
            yield return new WaitForSeconds(2f);
            defendiendo = false;
        }

    }

    public void recibirDaño(int daño)
    {
        if (!defendiendo)
        {
            puedeRealizarAcciones = false;
            animator.SetTrigger("Daño");
            Tiempo(3f);
            puedeRealizarAcciones = true;
        }
    }

    private IEnumerator Tiempo(float numero)
    {
        yield return new WaitForSeconds(numero);
    }


    public IEnumerator AtaqueAire()
    {
        puedeRealizarAcciones = false;
        animator.SetBool("Saltar", false);
        animator.SetBool("Caer", false);
        float gravity = rb.gravityScale;
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        rb.gravityScale = 0.1f;

        animator.SetTrigger("AirAtk");
        yield return new WaitForSeconds(1f);
        rb.gravityScale = gravity;
        puedeRealizarAcciones = true;
    }

    private IEnumerator AtkPrimario()
    {

        if (IsGrounded() && (Input.GetKeyDown(KeyCode.Q)))
        {
            atacando = true;
            puedeRealizarAcciones = false;
            animator.SetTrigger("Atk1");
            yield return new WaitForSeconds(TimeAtk1);
            atacando = false;
            puedeRealizarAcciones = true;

        }
        else if (IsGrounded() && Input.GetKey(KeyCode.Q))
        {

            atacando = true;
            puedeRealizarAcciones = false;
            animator.SetTrigger("Atk2");
            yield return new WaitForSeconds(TimeAtk2);
            atacando = false;
            puedeRealizarAcciones = true;

            if (IsGrounded() && Input.GetKey(KeyCode.Q))
            {

                atacando = true;
                puedeRealizarAcciones = false;
                animator.SetTrigger("Atk3");
                yield return new WaitForSeconds(TimeAtk3);
                atacando = false;
                puedeRealizarAcciones = true;
            }
            else
            {
                animator.SetTrigger("AtkFinal");
            }
        }
    }

    private IEnumerator AtkEspecial()
    {
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.E)))
        {
            atacando = true;
            puedeRealizarAcciones = false;
            animator.SetTrigger("SpecialAtk");
            yield return new WaitForSeconds(TimeSpecialAtk);
            atacando = false;
            puedeRealizarAcciones = true;
        }
    }


    protected virtual void TimeInitializer(){

    }

    protected virtual void MakeAtk1(){}
    protected virtual void MakeAtk2(){}
    protected virtual void MakeAtk3(){}
    protected virtual void MakeSpecialAtk(){}
    protected virtual void MakeAirAtk(){}
}
