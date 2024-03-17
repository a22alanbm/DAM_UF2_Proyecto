using UnityEngine;

public class SongController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip yourSong;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = yourSong;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        PlaySong();
    }

    // Método para reproducir la canción
    private void PlaySong()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes realizar otras operaciones de actualización aquí si es necesario
    }
}
