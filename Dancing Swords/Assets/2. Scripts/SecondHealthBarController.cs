using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondHealthBarController : MonoBehaviour
{
    public Image healthBarFill;

    private float maxHealth = 100f;
    private float currentHealth;
    private bool defenseEnabled = true;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // If both defense keys are held, disable defense for 2 seconds
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && defenseEnabled)
        {
            StartCoroutine(TemporarilyDisableDefense());
        }

        // Damage by W unless defended with A
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!Input.GetKey(KeyCode.A) || !defenseEnabled)
                TakeDamage(10f);
        }

        // Damage by S unless defended with D
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Input.GetKey(KeyCode.D) || !defenseEnabled)
                TakeDamage(10f);
        }
    }

    IEnumerator TemporarilyDisableDefense()
    {
        defenseEnabled = false;
        Debug.Log("Second player's defense disabled for 2 seconds due to invalid input!");
        yield return new WaitForSeconds(2f);
        defenseEnabled = true;
        Debug.Log("Second player's defense re-enabled.");
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
