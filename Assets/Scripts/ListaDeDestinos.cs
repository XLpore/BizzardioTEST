using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListaDeDestinos", menuName = "Portales/Lista de Destinos")]
public class ListaDeDestinos : ScriptableObject
{
    public List<Transform> destinos;
}