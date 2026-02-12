using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _bulletDamage = 1f;
    [SerializeField] private float _lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.position += transform.up * _bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        LifePlayer life = collision.GetComponent<LifePlayer>();
        if (life != null)
        {
            life.TakeDamage(_bulletDamage);
            Destroy(gameObject);
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
    }
}

