using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float life = 3f;

    [Header("Detección")]
    [SerializeField] private float detectionRadius = 2.5f;

    [Header("Explosión")]
    [SerializeField] private float chargeTime = 1.2f;
    [SerializeField] private float explosionDamage = 2f;
    [SerializeField] private float explosionRadius = 2f;

    private Transform player;
    private Animator anim;

    private bool charging = false;
    private bool exploded = false;

    private void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            player = playerGO.transform;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null || exploded) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Empieza a cargar cuando entra en el radio
        if (distance <= detectionRadius && !charging)
        {
            StartCharge();
        }
    }

    private void StartCharge()
    {
        charging = true;

        if (anim != null)
            anim.SetTrigger("Charge");

        StartCoroutine(ChargeAndExplode());
    }

    private IEnumerator ChargeAndExplode()
    {
        yield return new WaitForSeconds(chargeTime);
        Explode();
    }

    private void Explode()
    {
        if (exploded) return;
        exploded = true;

        if (anim != null)
            anim.SetTrigger("Explode");

        // Detecta todos los colliders en el radio de explosión
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            // DAÑO AL PLAYER
            PlayerDamageReceiver playerDamage = hit.GetComponent<PlayerDamageReceiver>();
            if (playerDamage != null)
            {
                playerDamage.ApplyDamage();
                playerDamage.ApplyDamage(); // 2 de daño
            }

            // DAÑO A ENEMIGOS
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }
        }

        // Destruye la TNT después de la animación de explosión
        Destroy(gameObject, 0.6f);
    }

    public void TakeDamage(float damage)
    {
        if (exploded) return;

        life -= damage;

        if (life <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            if (anim != null)
                anim.SetTrigger("Damage");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}