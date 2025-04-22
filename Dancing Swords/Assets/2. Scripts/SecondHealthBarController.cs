using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondHealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    public GameObject attackFeedbackImage; // assign via Inspector

    public float maxHealth = 100f;
    public float currentHealth;

    private bool wPowerUpActive = false;
    private bool sPowerUpActive = false;
    public GameObject winMessage; // drag your UI text/image into this field

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        attackFeedbackImage.SetActive(false);
    }

    void Update()
    {
        // Trigger power-ups
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!wPowerUpActive)
            {
                Debug.Log("W Power-Up Activated!");
                WorldSFXManager.instance.PlayBlockSound();
                StartCoroutine(ActivateWPowerUp());
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!sPowerUpActive)
            {
                Debug.Log("S Power-Up Activated!");
                WorldSFXManager.instance.PlayBlockSound();
                StartCoroutine(ActivateSPowerUp());
            }
        }

        // Attacks
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W Attack!");
            float damage = wPowerUpActive ? 20f : 5f;
            TakeDamage(damage);
            wPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S Attack!");
            float damage = sPowerUpActive ? 20f : 5f;
            TakeDamage(damage);
            sPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }
    }

    IEnumerator ActivateWPowerUp()
    {
        wPowerUpActive = true;
        yield return new WaitForSeconds(2f);
        wPowerUpActive = false;
    }

    IEnumerator ActivateSPowerUp()
    {
        sPowerUpActive = true;
        yield return new WaitForSeconds(2f);
        sPowerUpActive = false;
    }

    IEnumerator ShowAttackFeedback()
    {
        attackFeedbackImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackFeedbackImage.SetActive(false);
    }

    void TakeDamage(float amount)
    {
        if (amount > 10)
        {
            WorldSFXManager.instance.PlayLargeHitSound();
        }
        else
        {
            WorldSFXManager.instance.PlaySmallHitSound();
        }
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f && GetComponent<HealthBarController>().currentHealth >= 0)
        {
            Debug.Log("A wins!");
            winMessage.SetActive(true); // Show "A wins"
            enabled = false;
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
