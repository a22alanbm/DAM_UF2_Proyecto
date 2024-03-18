using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCharacter : Character
{
    public Transform controladorAtk1;
    public Transform controladorAtk2;
    public Transform controladorAtk3;
    public Transform controladorSpecialAtk;
    public Transform controladorAirAtk;
    private SpriteRenderer spriteRenderer;

    protected override void TimeInitializer()
    {
        TimeAtk1 = 0.3f;
        TimeAtk2 = 1.1f;
        TimeAtk3 = 1.0f;
        TimeSpecialAtk = 2.8f;
    }

    protected override void MakeAtk1()
    {
        if (controladorAtk1 != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            // Obtener la dirección del flip del SpriteRenderer
            int flipDirection = (spriteRenderer.flipX) ? -1 : 1;

            // Ajustar la posición del controlador de ataque según el flip del SpriteRenderer
            Vector3 adjustedPosition = controladorAtk1.position;
            adjustedPosition.x += 1.5f * flipDirection; // Ajustar la posición en el eje X

            // Realizar la detección de colisión usando el área de detección ajustada
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(adjustedPosition, new Vector2(1.5f, 0.1f), 0f);

            foreach (Collider2D hit in hitEnemies)
            {
                EnemyController enemyController = hit.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.RecibirDaño(10);
                }
            }
        }
    }
    protected override void MakeAtk2()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D[] hitEnemies;
        if (spriteRenderer.flipX){
            hitEnemies = Physics2D.OverlapBoxAll(controladorAtk2.position + new Vector3(-2.2f, 0, 0), new Vector2(2f, 0.4f), 0f);
        }else{
            hitEnemies = Physics2D.OverlapBoxAll(controladorAtk2.position, new Vector2(2f, 0.4f), 0f);
        }
        foreach (Collider2D hit in hitEnemies)
        {
            EnemyController enemyController = hit.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.RecibirDaño(15);
            }
        }
    }
    protected override void MakeAtk3()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Lista para almacenar todos los enemigos golpeados
        List<Collider2D> hitEnemiesList = new List<Collider2D>();
        int flipDirection = (spriteRenderer.flipX) ? -1 : 1;
        // Iterar a través de los segmentos del octavo de círculo
        Vector3 center = controladorAtk3.position; // Centro del octavo de círculo
        float radius = 2f; // Radio del octavo de círculo
        int segments = 10; // Número de segmentos para aproximar el octavo de círculo
        float angleStep = 45f / segments; // Ángulo entre cada segmento
        // Dibuja los segmentos del octavo de círculo
        if (!spriteRenderer.flipX)
        {
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleStep;
                Vector3 start = center + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;
                Vector3 end = center + Quaternion.Euler(0, 0, angle + angleStep) * Vector3.right * radius;
                // Obtener todos los enemigos dentro del segmento actual
                Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(start, end);
                // Agregar los enemigos golpeados a la lista
                hitEnemiesList.AddRange(hitEnemies);
                foreach (Collider2D hit in hitEnemiesList)
                {
                    EnemyController enemyController = hit.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        enemyController.RecibirDaño(20);
                    }
                }
            }
        }
        else 
        {
            center = center + new Vector3(-1.3f, 0, 0);
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleStep + 140f;
                Vector3 start = center + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;
                Vector3 end = center + Quaternion.Euler(0, 0, angle + angleStep) * Vector3.right * radius;
                // Obtener todos los enemigos dentro del segmento actual
                Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(start, end);
                // Agregar los enemigos golpeados a la lista
                hitEnemiesList.AddRange(hitEnemies);
                foreach (Collider2D hit in hitEnemiesList)
                {
                    EnemyController enemyController = hit.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        StartCoroutine(atkspecial(enemyController));
                    }
                }
            }
        }
    }
    
    IEnumerator atkspecial(EnemyController enemyController){
        enemyController.RecibirDaño(20);
        yield return new WaitForSeconds(1);
        enemyController.RecibirDaño(20);
    }

    protected override void MakeAirAtk()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int flipDirection = (spriteRenderer.flipX) ? -1 : 1;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(controladorAirAtk.position, new Vector2(2f * flipDirection, 0.4f), 0f);
        foreach (Collider2D hit in hitEnemies)
        {
            EnemyController enemyController = hit.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.RecibirDaño(10);
            }
        }
    }
    protected override void MakeSpecialAtk()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Determina la dirección del flip del sprite
        int flipDirection = (spriteRenderer.flipX) ? -1 : 1;

        // Calcula el radio del círculo de ataque
        float radius = 1.1f;

        // Calcula la posición del centro del círculo de ataque
        Vector2 center = controladorSpecialAtk.position;

        if (!spriteRenderer.flipX)
        {
            center = center + new Vector2(-2.6f, 0);
        }
        // Encuentra todos los colliders dentro del círculo de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(center, radius);

        // Aplica daño a los enemigos alcanzados por el ataque
        foreach (Collider2D hit in hitEnemies)
        {
            EnemyController enemyController = hit.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.RecibirDaño(50);
            }
        }
    }

    // Método para dibujar el gizmo del círculo de ataque en el editor
    private void OnDrawGizmosSelected()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Define el color del gizmo
        Gizmos.color = Color.red;

        // Determina la dirección del flip del sprite
        int flipDirection = (spriteRenderer != null && spriteRenderer.flipX) ? -1 : 1;

        // Define el radio del círculo de ataque
        float radius = 1.5f;

        // Calcula la posición del centro del círculo de ataque
        Vector3 center = controladorSpecialAtk.position;


        if (!spriteRenderer.flipX)
        {
            // Dibuja el gizmo del círculo de ataque
            Gizmos.DrawWireSphere(center, radius * Mathf.Abs(flipDirection));
        }
        else
        {
            center = center + new Vector3(-2.6f, 0, 0);
            // Dibuja el gizmo del círculo de ataque
            Gizmos.DrawWireSphere(center, radius * Mathf.Abs(flipDirection));
            center = center - new Vector3(-2.6f, 0, 0);
        }
        Gizmos.color = Color.green;
        // Define el centro y tamaño del rectángulo
        center = controladorAtk2.position;
        if (!spriteRenderer.flipX)
        {
            Vector3 mysize = new Vector3(2f, 0.4f, 0f); // Puedes ajustar el tamaño según tus necesidades
            Gizmos.DrawWireCube(center, mysize);
        }
        else
        {
            center = center + new Vector3(-2.2f, 0, 0);
            Vector3 mysize = new Vector3(2f, 0.4f, 0f); // Puedes ajustar el tamaño según tus necesidades
            Gizmos.DrawWireCube(center, mysize);
            center = center - new Vector3(-2.2f, 0, 0);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        Gizmos.color = Color.blue;

        // Parámetros del octavo de círculo
        center = controladorAtk3.position; // Centro del octavo de círculo
        radius = 2f; // Radio del octavo de círculo
        int segments = 10; // Número de segmentos para aproximar el octavo de círculo
        float angleStep = 45f / segments; // Ángulo entre cada segmento

        if (!spriteRenderer.flipX)
        {

            // Dibuja los segmentos del octavo de círculo
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleStep;
                Vector3 start = center + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;
                Vector3 end = center + Quaternion.Euler(0, 0, angle + angleStep) * Vector3.right * radius;
                Gizmos.DrawLine(center, start);
                Gizmos.DrawLine(start, end);
            }
        }
        else
        {
            center = center + new Vector3(-1.3f, 0, 0);
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleStep + 140f;
                Vector3 start = center + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;
                Vector3 end = center + Quaternion.Euler(0, 0, angle + angleStep) * Vector3.right * radius;
                Gizmos.DrawLine(center, start);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
