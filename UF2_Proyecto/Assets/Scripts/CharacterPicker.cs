using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterPicker : MonoBehaviour
{
    // Personaje serializado que se asignará al DataManager
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject blackFadeObject;
    [SerializeField] private Sprite characterIcon;
    [SerializeField] private Camera mainCamera; // Cámara a la que se le aplicará el efecto de desvanecimiento

    // Método llamado cuando se hace clic en el objeto
    private void OnMouseDown()
    {
        // Verifica si el botón izquierdo del ratón fue presionado
        if (Input.GetMouseButtonDown(0))
        {
            // Accede al DataManager y establece el personaje y el icono
            if (DataManager.Instance != null)
            {
                DataManager.Instance.SetCharacter(characterPrefab, characterIcon);
                // Cambia a la escena deseada
                CambiarEscena();
            }
            else
            {
                Debug.LogWarning("DataManager no está presente en la escena.");
            }
        }
    }

    // Función para cambiar de escena
    private void CambiarEscena()
    {
        if (blackFadeObject != null && mainCamera != null)
        {
            // Obtener el script de desvanecimiento del objeto
            fadeblackController fadeController = blackFadeObject.GetComponent<fadeblackController>();

            // Comenzar el desvanecimiento
            fadeController.FadeIn();

            // Acceder al SongController y hacer FadeOut
            SongController songController = FindObjectOfType<SongController>();
            if (songController != null)
            {
                songController.StartCoroutine(songController.FadeOut());
            }

            // Esperar 1 segundo antes de cargar la siguiente escena
            StartCoroutine(loadScene());
        }
        else
        {
            Debug.LogError("No se ha asignado un objeto de desvanecimiento o una cámara.");
        }
    }

    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
}
