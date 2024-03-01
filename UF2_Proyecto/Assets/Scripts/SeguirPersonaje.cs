using UnityEngine;

public class SeguirPersonaje : MonoBehaviour
{
    public Transform objetivo;
    public float suavizadoMinimo = 0.3f; // Suavizado mínimo
    public float suavizadoMaximo = 1.0f; // Suavizado máximo
    public float distanciaSinSuavizado = 5.0f; // Distancia a partir de la cual no hay suavizado

    private void FixedUpdate()
    {
        if (objetivo != null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            objetivo = playerObject.transform;

            // Calcular la distancia entre la cámara y el objetivo
            float distancia = Vector2.Distance(transform.position, objetivo.position);

            // Calcular la posición deseada de la cámara
            Vector3 posicionDeseada = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);

            // Ajustar la suavidad basada en la distancia
            float suavizado = Mathf.Lerp(suavizadoMinimo, suavizadoMaximo, Mathf.InverseLerp(0, distanciaSinSuavizado, distancia));

            // Suavizar el movimiento de la cámara
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        }
    }
}
