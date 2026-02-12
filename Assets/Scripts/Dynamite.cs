using System.Collections;
using System.Collections.Generic;
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

        // Animator en el sprite hijo
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (player == null || charging || exploded) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
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

        
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius
        );

        foreach (Collider2D hit in hits)
        {
            PlayerDamageReceiver player =
                hit.GetComponent<PlayerDamageReceiver>();

            if (player != null)
            {
                player.ApplyDamage();
                player.ApplyDamage(); // 2 de daño
            }

            // === FLASH DE PANTALLA ===
            PlayerCollisionTrigger flash =
                hit.GetComponent<PlayerCollisionTrigger>();

            if (flash != null)
            {
                flash.TriggerFromExplosion();
            }
        }


        Destroy(gameObject, 0.6f);
    }

    
    public void TakeDamage(float damage)
    {
        if (exploded) return;

        life -= damage;

        if (life <= 0f)
        {
            DieWithoutExplosion();
        }
        else
        {
            if (anim != null)
                anim.SetTrigger("Damage");
        }
    }

    private void DieWithoutExplosion()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}