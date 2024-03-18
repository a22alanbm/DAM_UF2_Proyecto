using System.Collections;
using UnityEngine;

public class SongController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip yourSong;
    private bool fadingOut = false;

    // Volumen específico para esta canción
    [SerializeField]
    private float specificVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = yourSong;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Ajustar el volumen específico de esta canción
        audioSource.volume = specificVolume;

        // Iniciar la reproducción de la canción
        PlaySong();

        // Iniciar el desvanecimiento gradual
        StartCoroutine(FadeIn());
    }

    // Método para reproducir la canción
    private void PlaySong()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Coroutine para desvanecer gradualmente el volumen hasta alcanzar el volumen normal
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeDuration = 3f; // Duración del desvanecimiento en segundos

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, specificVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        // Asegurar que el volumen sea exactamente el volumen específico al final del desvanecimiento
        audioSource.volume = specificVolume;
    }

    // Coroutine para desvanecer gradualmente el volumen hasta alcanzar 0
    public IEnumerator FadeOut()
    {
        fadingOut = true;

        float elapsedTime = 0f;
        float fadeDuration = 3f; // Duración del desvanecimiento en segundos
        float startVolume = audioSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        // Asegurar que el volumen sea exactamente 0 al final del desvanecimiento
        audioSource.volume = 0f;

        // Detener la reproducción de la canción después de desvanecerse
        audioSource.Stop();

        fadingOut = false;
    }

    // Método para iniciar el desvanecimiento hacia fuera desde otros objetos
    public void StartFadeOut()
    {
        if (!fadingOut)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes realizar otras operaciones de actualización aquí si es necesario
    }
}
