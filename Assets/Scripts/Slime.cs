using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private GameObject slimeHijo1;
    [SerializeField] private GameObject slimeHijo2;

    public RoomTrigger room;

    public void Morir()
    {
        Debug.Log("slime murio");

        Vector3 offset1 = new Vector3(-0.5f, 0, 0);
        Vector3 offset2 = new Vector3(0.5f, 0, 0);

        GameObject h1 = Instantiate(slimeHijo1, transform.position + offset1, Quaternion.identity);
        GameObject h2 = Instantiate(slimeHijo2, transform.position + offset2, Quaternion.identity);

        
        Enemy e1 = h1.GetComponent<Enemy>();
        Enemy e2 = h2.GetComponent<Enemy>();


        Destroy(gameObject);
    }
}
