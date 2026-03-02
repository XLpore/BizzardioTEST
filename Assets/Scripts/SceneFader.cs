using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying && fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }
#endif

    IEnumerator FadeIn()
    {
        float t = fadeDuration;
        Color c = fadeImage.color;

        while (t > 0)
        {
            t -= Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
    }
}