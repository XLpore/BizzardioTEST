using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    [SerializeField]
    private float CurrentLife = 5f;


    public void TakeDamage(float damage)
    {
        CurrentLife -= damage;

        if (CurrentLife <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
