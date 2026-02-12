using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRGB : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;        // Velocidad del cambio de color
    [SerializeField] private float saturation = 0.4f;   // Saturación (colores suaves)
    [SerializeField] private float brightness = 0.6f;   // Brillo (luminosidad moderada)

    private SpriteRenderer spriteRenderer;
    private float hue = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1f) hue -= 1f;

        Color newColor = Color.HSVToRGB(hue, saturation, brightness);
        spriteRenderer.color = newColor;
    }
}