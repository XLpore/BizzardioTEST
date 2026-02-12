using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletturret : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damage = 1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //  DAO A ENEMIGOS 
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        
        PlayerDamageReceiver player = other.GetComponent<PlayerDamageReceiver>();
        if (player != null)
        {
            player.ApplyDamage(); 
            Destroy(gameObject);
        }
    }
}