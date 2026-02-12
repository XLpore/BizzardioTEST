using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float lifeToAdd = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamageReceiver player = other.GetComponent<PlayerDamageReceiver>();

        if (player != null)
        {
            player.AddLife(lifeToAdd);
            Destroy(gameObject);
        }
    }

}
