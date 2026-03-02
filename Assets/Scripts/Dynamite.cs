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

        // Charge automático cuando el jugador se acerca
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

    // Pública para que el Enemy la llame o se dispare por proximidad
    public void Explode()
    {
        if (exploded) return;
        exploded = true;

        // Siempre Explode, sin importar cómo murió
        if (anim != null)
            anim.SetTrigger("Explode");

        // Desactivar Enemy para que no interfiera
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.StopAllCoroutines();
            enemy.enabled = false;
        }

        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            PlayerDamageReceiver playerDamage = hit.GetComponent<PlayerDamageReceiver>();
            if (playerDamage != null)
            {
                playerDamage.ApplyDamage();
                playerDamage.ApplyDamage(); // 2 de daño
            }

            Enemy otherEnemy = hit.GetComponent<Enemy>();
            if (otherEnemy != null && otherEnemy != GetComponent<Enemy>())
            {
                otherEnemy.TakeDamage(explosionDamage);
            }
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (exploded) return;

        life -= damage;

        if (life <= 0f)
        {
            Explode(); //  siempre Explode al morir
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