using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private Transform gunVisual; // arrastrá aquí "Gun Entero"

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // FLIP
        if (angle > 90 || angle < -90)
        {
            gunVisual.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            gunVisual.localScale = new Vector3(1, 1, 1);
        }
    }
}