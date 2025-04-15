using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
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
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && defenseEnabled)
        {
            StartCoroutine(TemporarilyDisableDefense());
        }

        // Only apply damage if not defended
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!Input.GetKey(KeyCode.LeftArrow) || !defenseEnabled)
                TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!Input.GetKey(KeyCode.RightArrow) || !defenseEnabled)
                TakeDamage(10f);
        }
    }

    IEnumerator TemporarilyDisableDefense()
    {
        defenseEnabled = false;
        Debug.Log("Defense disabled for 2 seconds due to invalid input!");
        yield return new WaitForSeconds(2f);
        defenseEnabled = true;
        Debug.Log("Defense re-enabled.");
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
