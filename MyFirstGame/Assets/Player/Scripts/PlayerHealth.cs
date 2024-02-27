using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public static PlayerHealth Instance;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private float health = 100f;
    private float maxHealth = 100f;
    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

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
            Die();
    }

    public void Heal(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        UpdateHealthUI();
    }

    private void Die()
    {
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        animator.SetBool("isDead", true);
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = health / maxHealth;
    }

    public void UsePotion(PotionObject potion)
    {
        foreach (var buff in potion.buff)
            ApplyBuff(buff);
    }

    public void ApplyBuff(ItemBuff buff)
    {
        switch (buff.attributes)
        {
            case Attributes.Health:
                Heal(buff.Value);
                break;
            case Attributes.Stamina:
                //Stamina(buff.Value)
                break;
            case Attributes.Strenght:
                break;
        }
    }
}
