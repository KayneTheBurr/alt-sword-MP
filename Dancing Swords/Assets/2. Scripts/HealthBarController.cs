using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    public GameObject attackFeedbackImage; // assign via Inspector

    public float maxHealth = 100f;
    public float currentHealth;

    public bool upPowerUpActive = false;
    public bool downPowerUpActive = false;
    public GameObject winMessage; // drag your UI text/image into this field

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        //attackFeedbackImage.SetActive(false);
    }

    void Update()
    {
        // Trigger power-ups
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!upPowerUpActive)
            {
                Debug.Log("Up Power-Up Activated!");
                WorldSFXManager.instance.PlayBlockSound();
                StartCoroutine(ActivateUpPowerUp());
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { if (!downPowerUpActive)
            {
                Debug.Log("Down Power-Up Activated!");
                WorldSFXManager.instance.PlayBlockSound();
                StartCoroutine(ActivateDownPowerUp());
            }
        }

        // Attacks
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow Attack!");
            float damage = GetComponent<SecondHealthBarController>().wPowerUpActive ? 20f : 5f;
            TakeDamage(damage);
            upPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow Attack!");
            float damage = GetComponent<SecondHealthBarController>().sPowerUpActive ? 20f : 5f;
            TakeDamage(damage);
            downPowerUpActive = false;
            StartCoroutine(ShowAttackFeedback());
        }
    }

    IEnumerator ActivateUpPowerUp()
    {
        upPowerUpActive = true;
        yield return new WaitForSeconds(2f);
        upPowerUpActive = false;
    }

    IEnumerator ActivateDownPowerUp()
    {
        downPowerUpActive = true;
        yield return new WaitForSeconds(2f);
        downPowerUpActive = false;
    }

    IEnumerator ShowAttackFeedback()
    {
        if (attackFeedbackImage != null)
        {
            Image img = attackFeedbackImage.GetComponent<Image>();
            if (img != null)
            {
                Color originalColor = img.color;
                img.color = Color.red;
                yield return new WaitForSeconds(1f);
                img.color = originalColor;
            }
        }
    }


    public void TakeDamage(float amount)
    {
        if(amount > 10)
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

        if (currentHealth <= 0f && GetComponent<SecondHealthBarController>().currentHealth >= 0)
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
