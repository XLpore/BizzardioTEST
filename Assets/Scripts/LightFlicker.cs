using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [Header("Intensidad")]
    [SerializeField] private float baseIntensity = 1f;
    [SerializeField] private float intensityVariation = 0.3f;

    [Header("Velocidad")]
    [SerializeField] private float flickerSpeed = 20f;

    private Light2D light2D;
    private float noiseOffset;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        noiseOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        float noise = Mathf.PerlinNoise(noiseOffset, Time.time * flickerSpeed);
        float intensity = baseIntensity + (noise - 0.5f) * intensityVariation;

        light2D.intensity = intensity;
    }
}

