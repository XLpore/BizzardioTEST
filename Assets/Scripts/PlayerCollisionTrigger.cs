using UnityEngine;

public class PlayerCollisionTrigger : MonoBehaviour
{
    [SerializeField] private ScreenFlash screenFlash;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TriggerFlash();
        }
    }

   
    public void TriggerFromExplosion()
    {
        TriggerFlash();
    }

    private void TriggerFlash()
    {
        if (screenFlash != null)
        {
            screenFlash.TriggerFlash();
        }
    }
}
