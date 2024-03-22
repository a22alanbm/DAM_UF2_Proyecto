using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField] private GameObject blackFadeObject; // Objeto con el script de desvanecimiento
    [SerializeField] private Camera mainCamera; // Cámara a la que se le aplicará el efecto de desvanecimiento

    private void OnMouseDown()
    {
        // Asegurarse de que se haya asignado un objeto de desvanecimiento y una cámara
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
            StartCoroutine(LoadScene());
        }
        else
        {
            Debug.LogError("No se ha asignado un objeto de desvanecimiento o una cámara.");
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
