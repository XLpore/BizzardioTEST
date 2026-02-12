using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [Header("Puertas del cuarto")]
    [SerializeField] private List<GameObject> puertas;

    private int enemigosVivos = 0;
    private bool jugadorDentro = false;

    void Start()
    {
        foreach (GameObject puerta in puertas)
        {
            if (puerta != null)
                puerta.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;

            if (enemigosVivos > 0)
            {
                ActivarPuertas(true);
                Debug.Log("Jugador entró. Puertas cerradas.");
            }
        }

        
        if (other.CompareTag("Enemy"))
        {
            enemigosVivos++;
            Debug.Log("Enemigo entró al cuarto. Total: " + enemigosVivos);

            if (jugadorDentro)
            {
                ActivarPuertas(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemigosVivos = Mathf.Max(0, enemigosVivos - 1);
            Debug.Log("Enemigo salió/murió. Total: " + enemigosVivos);

            if (enemigosVivos == 0 && jugadorDentro)
            {
                ActivarPuertas(false);
                Debug.Log("Cuarto limpio. Puertas abiertas.");
            }
        }
    }

    private void ActivarPuertas(bool estado)
    {
        foreach (GameObject puerta in puertas)
        {
            if (puerta != null)
                puerta.SetActive(estado);
        }
    }
}

