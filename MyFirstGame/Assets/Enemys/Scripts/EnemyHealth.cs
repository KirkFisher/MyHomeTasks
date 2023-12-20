using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float totalHealth = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    private float health;


    private void Start()
    {
        health = totalHealth;
        animator = GetComponent<Animator>();
        InitHealth();
    }
    public void ReduceHealth(float damage)
    {
        health -= damage;
        InitHealth();
        animator.SetTrigger("takeDamage");
        if (health <= 0)
        {
            Die();
            
        }
    }
    private void InitHealth()
    {
        slider.value = health / totalHealth;
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
