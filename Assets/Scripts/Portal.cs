using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public ListaDeDestinos listaDeDestinos; 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || listaDeDestinos == null || listaDeDestinos.destinos.Count == 0)
            return;

        int indice = Random.Range(0, listaDeDestinos.destinos.Count);
        Transform destinoAleatorio = listaDeDestinos.destinos[indice];
        col.transform.position = destinoAleatorio.position;

        
        listaDeDestinos.destinos.RemoveAt(indice);
    }
}