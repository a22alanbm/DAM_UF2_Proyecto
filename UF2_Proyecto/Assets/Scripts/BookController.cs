using System.Collections;
using UnityEngine;

public class BookController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject[] objectsToShow;
    [SerializeField] public bool doesitstart = false;
    [SerializeField] public GameObject objectToDeactivate; // Objeto a desactivar
    [SerializeField] public GameObject objectToActivate;   // Objeto a activar
    private bool empezado; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (GetComponent<BoxCollider2D>() == null)
        {
            Debug.LogError("Se requiere un BoxCollider2D para detectar clics. Agrega un BoxCollider2D al objeto.");
        }

        if (doesitstart && ! empezado){
            empezado=true;
            StartCoroutine(SwitchObjects());
            animator.SetTrigger("Open");
        }

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

    // Método llamado desde la animación para desactivar y activar objetos
    private IEnumerator SwitchObjects()
    {
        // Desactiva el objectToDeactivate
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
        // Activa el objectToActivate
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        yield return new WaitForSeconds(0.2f);
        showAll();
    }

    private void showAll()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes realizar otras operaciones de actualización aquí si es necesario
        if (doesitstart&& ! empezado){
            empezado=true;
            StartCoroutine(SwitchObjects());
            animator.SetTrigger("Open");
        }
    }
}
