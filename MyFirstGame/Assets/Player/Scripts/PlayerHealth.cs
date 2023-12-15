using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private float health = 100f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ReduceHealth(float damage)
    {
        health -= damage;
        animator.SetTrigger("takeDamage");
        if (health <= 0)
        {
            Die();
            
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
