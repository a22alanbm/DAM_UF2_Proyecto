using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameCloser : MonoBehaviour
{
    // Tiempo en segundos antes de cerrar el juego
    public float tiempoDeEspera = 200.0f; // 3 minutos y 20 segundos

    [SerializeField] private GameObject blackFadeObject; // Objeto con el script de desvanecimiento
    [SerializeField] private Camera mainCamera; // Cámara a la que se le aplicará el efecto de desvanecimiento
    [SerializeField] private DataManager dataManager; 
    private float tiempoTranscurrido = 0.0f;

    void Update()
    {

        // Incrementa el tiempo transcurrido
        tiempoTranscurrido += Time.deltaTime;

        // Comprueba si el tiempo transcurrido es mayor o igual al tiempo de espera
        if (tiempoTranscurrido >= tiempoDeEspera)
        {
            StartCoroutine(LoadMyScene());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadMyScene());
        }

    }



    private IEnumerator LoadMyScene()
    {
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
        dataManager.ResetData();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}