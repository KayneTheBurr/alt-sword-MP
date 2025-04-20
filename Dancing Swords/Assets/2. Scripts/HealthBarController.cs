using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    public GameObject attackFeedbackImage; // assign via Inspector

    public float maxHealth = 100f;
    public float currentHealth;

    private bool upPowerUpActive = false;
    private bool downPowerUpActive = false;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Up Power-Up Activated!");
            WorldSFXManager.instance.PlayBlockSound();
            StartCoroutine(ActivateUpPowerUp());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Down Power-Up Activated!");
            WorldSFXManager.instance.PlayBlockSound();
            StartCoroutine(ActivateDownPowerUp());
        }

        // Attacks
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow Attack!");
            float damage = upPowerUpActive ? 20f : 10f;
            TakeDamage(damage);
            upPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow Attack!");
            float damage = downPowerUpActive ? 20f : 10f;
            TakeDamage(damage);
            downPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }
    }

    IEnumerator ActivateUpPowerUp()
    {
        upPowerUpActive = true;
        yield return new WaitForSeconds(1f);
        upPowerUpActive = false;
    }

    IEnumerator ActivateDownPowerUp()
    {
        downPowerUpActive = true;
        yield return new WaitForSeconds(1f);
        downPowerUpActive = false;
    }

    IEnumerator ShowAttackFeedback()
    {
        attackFeedbackImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackFeedbackImage.SetActive(false);
    }

    void TakeDamage(float amount)
    {
        WorldSFXManager.instance.PlayHitSound();
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Debug.Log("B wins!");
            winMessage.SetActive(true); // Show "B wins"
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
