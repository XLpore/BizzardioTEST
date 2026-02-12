using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamageReceiver : MonoBehaviour
{
    [SerializeField] private float maxLife = 5f;
    private float currentLife;

    [SerializeField] private float damagePerHit = 1f;
    [SerializeField] private float damageInterval = 1f;

    private float damageTimer = 0f;
    private bool isInCooldown = false;

    [SerializeField] private Image[] heartImages;
    [SerializeField] private Text cooldownText; 

    [SerializeField] private Gunner gunner;  

    private void Start()
    {
        currentLife = maxLife;
        UpdateHeartsUI();
        cooldownText.text = ""; 

        UpdateColorStateBasedOnLife(); 
    }

    private void Update()
    {
        
        if (isInCooldown)
        {
            damageTimer -= Time.deltaTime;
            cooldownText.text = "Escudo " + damageTimer.ToString("F1") + "s";

            if (damageTimer <= 0f)
            {
                isInCooldown = false;
                cooldownText.text = ""; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInCooldown)
        {
            ApplyDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInCooldown)
        {
            ApplyDamage();
        }
    }

    public void ApplyDamage()
    {
        currentLife -= damagePerHit;
        if (currentLife < 0) currentLife = 0;

        Debug.Log("Player Life: " + currentLife);
        UpdateHeartsUI();

        UpdateColorStateBasedOnLife();

        if (currentLife <= 0f)
        {
            RestartScene();
        }
        else
        {
            isInCooldown = true;
            damageTimer = damageInterval;
        }
    }

    public void AddLife(float amount)
    {
        currentLife += amount;
        if (currentLife > maxLife) currentLife = maxLife;

        UpdateHeartsUI();

        UpdateColorStateBasedOnLife();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentLife;
        }
    }

    private void UpdateColorStateBasedOnLife()
    {
        if (currentLife >= 4)
        {
            gunner.CambiarColor(ColorState.Green); // 4 y 5 vidas
        }
        else if (currentLife == 2 || currentLife == 3)
        {
            gunner.CambiarColor(ColorState.Red);  //  2 y 3 vidas
        }
        else if (currentLife == 1)
        {
            gunner.CambiarColor(ColorState.Blue);  //1 vida
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
