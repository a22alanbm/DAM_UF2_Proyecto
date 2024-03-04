using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip yourSong;

    public GameObject objectToDeactivate; // Objeto a desactivar
    public GameObject objectToActivate;   // Objeto a activar

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (GetComponent<BoxCollider2D>() == null)
        {
            Debug.LogError("Se requiere un BoxCollider2D para detectar clics. Agrega un BoxCollider2D al objeto.");
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = yourSong;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        PlaySong();
    }

    // Método llamado cuando se hace clic en el objeto
    private void OnMouseDown()
    {
        // Verifica si el botón izquierdo del ratón fue presionado y el libro no está en proceso de apertura
        if (Input.GetMouseButtonDown(0))
        {
            // Activa el trigger "Open" en el Animator
            StartCoroutine(SwitchObjects());
            animator.SetTrigger("Open");
        }
    }

    // Método para reproducir la canción
    private void PlaySong()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Método llamado desde la animación para desactivar y activar objetos
    private IEnumerator SwitchObjects()
    {
        // Desactiva el objetoToDeactivate
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
        // Activa el objetoToActivate
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes realizar otras operaciones de actualización aquí si es necesario
    }
}
