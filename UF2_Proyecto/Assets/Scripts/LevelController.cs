using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpriteChanger : MonoBehaviour
{
    public Sprite[] levelSprites; // Arreglo de sprites para cada nivel
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private DataManager dataManager; // Referencia al DataManager

    [SerializeField] private GameObject blackFadeObject; // Objeto con el script de desvanecimiento
    [SerializeField] private Camera mainCamera; // Cámara a la que se le aplicará el efecto de desvanecimiento

    private void Start()
    {
        // Obtener el componente SpriteRenderer del GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtener la instancia del DataManager
        dataManager = DataManager.Instance;

        // Verificar si el DataManager es nulo
        if (dataManager == null)
        {
            Debug.LogError("No se encontró el DataManager en la escena.");
        }

        // Verificar si el SpriteRenderer no está asignado
        if (spriteRenderer == null)
        {
            Debug.LogError("No se ha encontrado el componente SpriteRenderer en este GameObject.");
        }
    }

    private void Update()
    {
        // Verificar si el DataManager es nulo o el SpriteRenderer no está asignado
        if (dataManager == null || spriteRenderer == null)
        {
            return;
        }

        // Obtener el nivel actual del DataManager
        int level = dataManager.GetLevel();
        if (level == 5)
        {
            StartCoroutine(Level5());
        }

        // Verificar si el nivel está dentro del rango de sprites disponibles
        if (level >= 0 && level < levelSprites.Length)
        {
            // Establecer el sprite correspondiente al nivel
            spriteRenderer.sprite = levelSprites[level];
        }
        else
        {
            Debug.LogError("El nivel está fuera del rango de sprites disponibles.");
        }
    }

    private IEnumerator Level5()
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
                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene(1);

            }
    }
}
