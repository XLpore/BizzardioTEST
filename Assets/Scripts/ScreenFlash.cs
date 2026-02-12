using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Image flashImage;          
    [SerializeField] private float flashDuration = 0.2f; 
    [SerializeField] private Color flashColor = new Color(1, 0, 0, 0.5f); 

    private Coroutine flashCoroutine;

    public void TriggerFlash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        flashImage.color = flashColor;

        float time = 0f;
        Color startColor = flashColor;
        Color endColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);

        while (time < flashDuration)
        {
            time += Time.deltaTime;
            flashImage.color = Color.Lerp(startColor, endColor, time / flashDuration);
            yield return null;
        }

        flashImage.color = endColor;


    }
}