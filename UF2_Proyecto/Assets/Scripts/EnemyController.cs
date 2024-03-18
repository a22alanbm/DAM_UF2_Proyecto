using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private int type; // Objeto con el script de desvanecimiento
    [SerializeField] public float speed = 3f; // Velocidad de movimiento del enemigo
    [SerializeField] public float attackRange = 1.5f; // Rango de ataque del enemigo
    [SerializeField] public int damage = 10; // Daño que inflige el enemigo al atacar
    [SerializeField] public int vida = 10; // Vida del enemigo
    [SerializeField] public int experienciaGanada = 1; // Experiencia ganada al derrotar al enemigo
    [SerializeField] public LayerMask playerLayer; // Capa que identifica al jugador
    [SerializeField] float cooldown = 5f; // Temporizador para el ataque

    private Transform player; // Referencia al transform del jugador
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del enemigo
    private bool canAttack = true; // Indica si el enemigo puede realizar un ataque

    private Animator animator;

    private EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        // Buscar al jugador al comienzo del juego
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("No se encontró al jugador en la escena. Asegúrate de etiquetar al jugador con la etiqueta 'Player'.");
        }

        // Obtener el componente SpriteRenderer del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        // Calcular la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Moverse solo en el eje X hacia el jugador
        transform.Translate(new Vector3(direction.x, 0, 0) * speed * Time.deltaTime);

        // Voltear el sprite del enemigo si se está moviendo hacia la izquierda
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Voltear el sprite del enemigo si se está moviendo hacia la derecha
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Si el jugador está dentro del rango de ataque y el enemigo puede atacar
        if (Vector3.Distance(transform.position, player.position) <= attackRange && canAttack)
        {
            // Atacar al jugador
            StartCoroutine(AttackCooldown());
        }
    }

    // Método para atacar al jugador
    void AttackPlayer()
    {
        // Llamar a la función recibirDaño del jugador
        animator.SetTrigger("Atk");
        player.GetComponent<Character>().recibirDaño(damage);
    }

    // Coroutine para el cooldown de ataque
    IEnumerator AttackCooldown()
    {
        // Deshabilitar el ataque
        canAttack = false;

        // Atacar al jugador
        AttackPlayer();

        // Esperar el tiempo de cooldown
        yield return new WaitForSeconds(cooldown);

        // Habilitar el ataque después del cooldown
        canAttack = true;
    }

    // Método para agregar experiencia al jugador
    void AgregarExperiencia()
    {
        // Asegurarse de que el DataManager existe
        if (DataManager.Instance != null)
        {
            // Agregar la experiencia ganada al jugador
            DataManager.Instance.SumarExperiencia(experienciaGanada);
        }
        else
        {
            Debug.LogError("No se encontró DataManager en la escena.");
        }
    }

    // Método para recibir daño
    public void RecibirDaño(int cantidad)
    {
        // Reducir la vida del enemigo
        vida -= cantidad;

        // Verificar si el enemigo ha sido derrotado
        if (vida <= 0)
        {
            // Agregar experiencia al jugador al derrotar al enemigo
            AgregarExperiencia();

            StartCoroutine(tiempo());
            
        }
    }
    IEnumerator tiempo(){
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(2);

            spawner.EnemyDestroyed(type);
            Destroy(gameObject);
    }
    public void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }
}
