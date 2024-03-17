using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField]
    private GameObject blackFadeObject; // Objeto con el script de desvanecimiento

    private void OnMouseDown()
    {
        // Asegurarse de que se haya asignado un objeto de desvanecimiento
        if (blackFadeObject != null)
        {
            // Obtener el script de desvanecimiento del objeto
            fadeblackController fadeController = blackFadeObject.GetComponent<fadeblackController>();

            // Comenzar el desvanecimiento
            fadeController.FadeIn();
            // esperar 1 segundo antes de cargar la siguiente escena
            StartCoroutine(loadScene());
        }
        else
        {
            Debug.LogError("No se ha asignado un objeto de desvanecimiento.");
        }
    }

    private IEnumerator loadScene(){
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
