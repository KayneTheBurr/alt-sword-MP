using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondHealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    public GameObject attackFeedbackImage; // assign via Inspector

    public float maxHealth = 100f;
    public float currentHealth;

    public bool wPowerUpActive = false;
    public bool sPowerUpActive = false;
    public GameObject winMessage; // drag your UI text/image into this field

    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        // 保存原始颜色
        if (attackFeedbackImage != null)
        {
            Image img = attackFeedbackImage.GetComponent<Image>();
            if (img != null)
            {
                originalColor = img.color;
            }
        }
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
            float damage = GetComponent<HealthBarController>().upPowerUpActive ? 20f : 5f;
            TakeDamage(damage);
            wPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S Attack!");
            float damage = GetComponent<HealthBarController>().downPowerUpActive ? 20f : 5f;
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
        if (attackFeedbackImage != null)
        {
            Image img = attackFeedbackImage.GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.red;
                yield return new WaitForSeconds(1f);
                img.color = originalColor;
            }
        }
    }

    public void TakeDamage(float amount)
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
