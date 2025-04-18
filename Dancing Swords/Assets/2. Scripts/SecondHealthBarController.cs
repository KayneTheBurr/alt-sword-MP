using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondHealthBarController : MonoBehaviour
{
    public Image healthBarFill;

    private float maxHealth = 100f;
    private float currentHealth;

    private bool defenseEnabled = true;
    private bool isLeftDefending = false;
    private bool isRightDefending = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Trigger defense on key press
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(TemporaryLeftDefense());

        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(TemporaryRightDefense());

        // Punish if both defense keys are pressed in the same frame
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D) && defenseEnabled)
        {
            StartCoroutine(TemporarilyDisableDefense());
        }

        // Damage by W unless defended
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isLeftDefending || !defenseEnabled)
                TakeDamage(10f);
        }

        // Damage by S unless defended
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRightDefending || !defenseEnabled)
                TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("w");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("s");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");

        }
    }

    IEnumerator TemporaryLeftDefense()
    {
        isLeftDefending = true;
        yield return new WaitForSeconds(1f);
        isLeftDefending = false;
    }

    IEnumerator TemporaryRightDefense()
    {
        isRightDefending = true;
        yield return new WaitForSeconds(1f);
        isRightDefending = false;
    }

    IEnumerator TemporarilyDisableDefense()
    {
        defenseEnabled = false;
        Debug.Log("Second player's defense disabled for 4 seconds due to invalid input!");
        yield return new WaitForSeconds(4f);
        defenseEnabled = true;
        Debug.Log("Second player's defense re-enabled.");
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Debug.Log("A wins!");
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
