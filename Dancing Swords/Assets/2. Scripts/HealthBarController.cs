using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill;

    private float maxHealth = 100f;
    private float currentHealth;

    private bool defenseEnabled = true;
    private bool isLeftDefending = false;
    private bool isRightDefending = false;
    private float leftDefenseTime = -10f;
    private float rightDefenseTime = -10f;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Trigger defense on key press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            StartCoroutine(TemporaryLeftDefense());

        if (Input.GetKeyDown(KeyCode.RightArrow))
            StartCoroutine(TemporaryRightDefense());

        // Punish if both defense keys are pressed in the same frame
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow) && defenseEnabled)
        {
            StartCoroutine(TemporarilyDisableDefense());
        }

        // Damage by UpArrow unless defended
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isLeftDefending || !defenseEnabled)
                TakeDamage(10f);
        }

        // Damage by DownArrow unless defended
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isRightDefending || !defenseEnabled)
                TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow");

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("LeftArrow");

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("RightArrow");

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
        Debug.Log("Defense disabled for 4 seconds due to invalid input!");
        yield return new WaitForSeconds(4f);
        defenseEnabled = true;
        Debug.Log("Defense re-enabled.");
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Debug.Log("B wins!");
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
