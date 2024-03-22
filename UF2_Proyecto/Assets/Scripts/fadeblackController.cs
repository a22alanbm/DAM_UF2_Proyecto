using UnityEngine;
using System.Collections;

public class fadeblackController : MonoBehaviour
{
    public float fadeDuration = 1f;
    private SpriteRenderer spriteRenderer;
    private float startAlpha;
    private float endAlpha;
    private Coroutine fadeCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer en el GameObject.");
            return; // Salir del método si no se encuentra el componente SpriteRenderer
        }

        // Valor inicial de alfa (completamente opaco)
        startAlpha = 1f;
        // Valor final de alfa (completamente transparente)
        endAlpha = 0f;

        // Iniciar el desvanecimiento al comienzo
        FadeOut();
    }

    IEnumerator FadeAlpha(float targetAlpha)
    {
        float elapsedTime = 0f;
        float currentAlpha = spriteRenderer.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);
    }

    public void FadeOut()
    {
        StartCoroutine(tiempo());
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAlpha(endAlpha));
    }

    public void FadeIn()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAlpha(startAlpha));
    }

    IEnumerator tiempo(){
        yield return new WaitForSeconds(0.5f);
    }
}
