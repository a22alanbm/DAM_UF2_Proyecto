using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Propiedad estática para acceder al singleton desde cualquier lugar
    public static DataManager Instance { get; private set; }

    // Datos que quieres mantener entre escenas
    // En este caso, el personaje, el ícono del personaje, el nivel y la experiencia
    public GameObject character;
    public Sprite characterIcon;
    public int level;
    public int experiencia;

    private void Awake()
    {
        // Asegúrate de que solo haya una instancia del DataManager en la escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruir este objeto
        }
    }

    // Método para establecer el personaje, su ícono, el nivel y la experiencia
    public void SetCharacter(GameObject characterObject, Sprite icon)
    {
        character = characterObject;
        characterIcon = icon;
    }

    // Método para obtener el personaje
    public GameObject GetCharacter()
    {
        return character;
    }

    // Método para obtener el ícono del personaje
    public Sprite GetCharacterIcon()
    {
        return characterIcon;
    }

    // Método para obtener el nivel
    public int GetLevel()
    {
        return level;
    }

    // Método para establecer el nivel
    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    // Método para obtener la experiencia
    public int GetExperiencia()
    {
        return experiencia;
    }

    // Método para sumar experiencia
    public void SumarExperiencia(int cantidad)
    {
        experiencia += cantidad;
        ActualizarNivel();
    }

    // Método para actualizar el nivel basado en la experiencia
    private void ActualizarNivel()
    {
        if (experiencia >= 500)
        {
            level = 5;
        }
        if (experiencia >= 250)
        {
            level = 4;
        }
        else if (experiencia >= 100)
        {
            level = 3;
        }
        else if (experiencia >= 50)
        {
            level = 2;
        }
        else if (experiencia >= 10)
        {
            level = 1;
        }
    }

    // Método para reiniciar todos los datos a sus valores predeterminados
    public void ResetData()
    {
        character = null;
        characterIcon = null;
        level = 0;
        experiencia = 0;
    }
}
