using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField]private float health = 100f;
    private float maxHealth = 100f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        UpdateHealthUI();
    }
    

    public void ReduceHealth(float damage)
    {
        health -= damage;
        UpdateHealthUI();
        animator.SetTrigger("takeDamage");
        if (health <= 0)
        {
            Die();
            
        }
    }
    private void Heal(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
    }
    private void Die()
    {
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        animator.SetBool("isDead", true);
    }
    private void UpdateHealthUI()
    {
        // Обновление значения на UI Slider
        healthSlider.value = health / maxHealth;
    }
}
