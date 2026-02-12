using UnityEngine;

public class DisparoControladoPorColor : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;

    private Renderer rend;
    private float tiempoEntreDisparos;
    private float tiempoUltimoDisparo;

    void Start()
    {
        rend = GetComponent<Renderer>();
        CambiarColor(Color.green);  // Empezamos en verde
    }

    void Update()
    {
        // Cambiar color con la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (rend.material.color == Color.green)
                CambiarColor(Color.blue);
            else
                CambiarColor(Color.green);
        }

        // Disparar con barra espaciadora
        if (Input.GetKey(KeyCode.Space) && Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void CambiarColor(Color nuevoColor)
    {
        rend.material.color = nuevoColor;

        if (nuevoColor == Color.green)
            tiempoEntreDisparos = 0.2f; // Disparo rápido
        else if (nuevoColor == Color.blue)
            tiempoEntreDisparos = 1.0f; // Disparo lento
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
    }
}

