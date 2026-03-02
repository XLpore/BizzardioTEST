using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator _anim;

    [SerializeField] private float speed;
    [SerializeField] private float EnemyVision;
    [SerializeField] private float CurrentLife = 5;

    [Header("Prefabs de slimes hijos")]
    [SerializeField] private GameObject slimeHijo1;
    [SerializeField] private GameObject slimeHijo2;

    private Transform target;
    private NavMeshAgent agent;
    private Vector3 direction;

    [SerializeField] private PlayerDamageReceiver playerDamageReceiver;

    private bool muerto = false;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();

        if (_anim != null)
            _anim.SetInteger("Life", Mathf.FloorToInt(CurrentLife));

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speed;
        }

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            target = playerGO.transform;

            if (playerDamageReceiver == null)
                playerDamageReceiver = playerGO.GetComponent<PlayerDamageReceiver>();
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag Player.");
        }
    }

    private void Update()
    {
        if (target == null || muerto || agent == null) return;

        direction = target.position - transform.position;

        if (direction.magnitude > 0.5f && direction.magnitude < EnemyVision)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement playerCollision = collision.GetComponent<Movement>();

        if (playerCollision != null)
            playerCollision.Push(direction.normalized);
    }

    public void TakeDamage(float damage)
    {
        if (muerto || CurrentLife <= 0f) return;

        CurrentLife -= damage;

        if (_anim != null)
            _anim.SetInteger("Life", Mathf.FloorToInt(CurrentLife));

        if (CurrentLife > 0f)
        {
            if (_anim != null)
            {
                _anim.SetTrigger("Damage");
                Invoke(nameof(ReturnToIdle), 0.01f);
            }
        }
        else
        {
            //  Si es TNT, llamar Explode en lugar de Die
            Dynamite dyn = GetComponent<Dynamite>();
            if (dyn != null)
            {
                dyn.Explode(); // siempre Explode
            }
            else
            {
                StartCoroutine(DieAndDestroy());
            }
        }
    }

    private IEnumerator DieAndDestroy()
    {
        muerto = true;

        if (_anim != null)
            _anim.SetTrigger("Die");

        if (agent != null)
            agent.enabled = false;

        // Chance 1 de 10 de dar vida al jugador
        if (playerDamageReceiver != null)
        {
            int chance = Random.Range(1, 11);
            if (chance == 1)
                playerDamageReceiver.AddLife(1);
        }

        // Spawn de slimes hijos
        Vector3 offset1 = new Vector3(-0.5f, 0, 0);
        Vector3 offset2 = new Vector3(0.5f, 0, 0);

        if (slimeHijo1 && slimeHijo2)
        {
            Instantiate(slimeHijo1, transform.position + offset1, Quaternion.identity);
            Instantiate(slimeHijo2, transform.position + offset2, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void ReturnToIdle()
    {
        if (_anim != null)
            _anim.SetTrigger("BackToIdle");
    }
}