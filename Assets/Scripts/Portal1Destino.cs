using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1Destino : MonoBehaviour
{
    public Transform destino; 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") || destino == null)
            return;

        Vector3 nuevaPos = new Vector3(destino.position.x, destino.position.y, col.transform.position.z);
        col.transform.position = nuevaPos;

    }
}