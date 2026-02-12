using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemigos;
    [SerializeField] private GameObject portalVisual;

    void Start()
    {
        if (portalVisual != null)
            portalVisual.SetActive(false);
    }

    void Update()
    {
        enemigos.RemoveAll(e => e == null);

        if (enemigos.Count == 0 && portalVisual != null && !portalVisual.activeSelf)
        {
            portalVisual.SetActive(true);
        }
    }
}