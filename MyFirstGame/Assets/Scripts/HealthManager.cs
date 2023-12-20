using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Heal(float healAmount)
    {
        currentHealth =Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void UpdateHealthUI()
    {
        // Обновление значения на UI Slider
        healthSlider.value = currentHealth / maxHealth;
    }
}
