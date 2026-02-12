using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousemovement : MonoBehaviour
{

    [SerializeField]
    GameObject _mouseObject;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - _mouseObject.transform.position;

        _mouseObject.transform.right = direction;



    }

}
