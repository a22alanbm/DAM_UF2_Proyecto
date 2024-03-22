using UnityEngine;

public class IconRetreiver : MonoBehaviour
{
    private SpriteRenderer iconRenderer;

    private void Start()
    {
        // Obtener el SpriteRenderer del objeto actual
        iconRenderer = GetComponent<SpriteRenderer>();

        if (iconRenderer == null)
        {
            Debug.LogWarning("No se encontró un SpriteRenderer en el objeto " + gameObject.name);
            return;
        }

        // Recuperar el icono del DataManager
        Sprite icon = DataManager.Instance.GetCharacterIcon();

        if (icon != null)
        {
            // Asignar el sprite al Sprite Renderer
            iconRenderer.sprite = icon;
        }
        else
        {
            Debug.LogWarning("No se encontró un icono en el DataManager.");
        }
    }
}
