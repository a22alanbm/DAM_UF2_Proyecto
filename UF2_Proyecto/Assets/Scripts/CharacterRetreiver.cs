using UnityEngine;

public class CharacterRetreiver : MonoBehaviour
{
    private void Start()
    {
        // Recuperar el personaje del DataManager
        GameObject characterObject = DataManager.Instance.GetCharacter();

        if (characterObject != null)
        {
            // Copiar todos los componentes del personaje recuperado al GameObject actual
            CopyComponents(characterObject, gameObject);

            // Desactivar el script en este objeto, ya que ahora está siendo utilizado por el personaje
            enabled = false;
        }
        else
        {
            Debug.LogWarning("No se encontró un personaje en el DataManager.");
        }
    }

    private void CopyComponents(GameObject source, GameObject target)
    {
        // Copiar la transformación
        //target.transform.position = source.transform.position;
        //target.transform.rotation = source.transform.rotation;
        target.transform.localScale = source.transform.localScale;

        // Copiar todos los componentes
        Component[] components = source.GetComponents<Component>();
        foreach (Component component in components)
        {
            // Asegurarse de no copiar el Transform ya que ya lo hemos manejado
            if (!(component is Transform))
            {
                // Clonar el componente
                Component newComponent = target.AddComponent(component.GetType());

                // Copiar los valores de los campos públicos y propiedades del componente original al nuevo componente
                UnityEditor.EditorUtility.CopySerialized(component, newComponent);
            }
        }
    }
}
