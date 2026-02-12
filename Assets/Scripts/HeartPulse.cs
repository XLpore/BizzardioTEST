using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [Header("Pulso")]
    [SerializeField] private float pulseSpeed = 2f;   
    [SerializeField] private float pulseScale = 0.15f; 

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
        transform.localScale = initialScale + Vector3.one * scaleOffset;
    }
}
