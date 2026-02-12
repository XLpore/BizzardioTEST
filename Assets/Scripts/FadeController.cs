using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour

{
    [SerializeField] private float fadeDuration = 1f;   // Tiempo para desvanecer y aparecer
    [SerializeField] private float waitDuration = 2f;   // Tiempo de espera entre transiciones

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            // Desvanecer
            yield return StartCoroutine(FadeTo(0.15f, fadeDuration));

            // Esperar invisible
            yield return new WaitForSeconds(waitDuration);

            // Aparecer
            yield return StartCoroutine(FadeTo(1f, fadeDuration));

           
            yield return new WaitForSeconds(waitDuration);
        }
    }

    IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = spriteRenderer.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}