using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    [SerializeField] public GameObject objectToActivate;

    void Start()
    {
        // Oculta el objeto al iniciar el juego
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void Update()
    {
        // Activa/desactiva el objeto al pulsar la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(!objectToActivate.activeSelf);
        }
        StartCoroutine(tiempo());
        // Pausa o reanuda el juego al activar/desactivar el Time.timeScale
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        // Activa/desactiva el objeto al pulsar el botón o la tecla "Escape"
        
    }
    IEnumerator tiempo(){
        yield return new WaitForSeconds(3f);
    }
}
