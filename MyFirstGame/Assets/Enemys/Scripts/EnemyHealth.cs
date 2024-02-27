using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float totalHealth = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private AudioSource enemyHitSound;
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
        enemyHitSound.Play();
        if (health <= 0)
        {
            Die();
        }
        Invoke("Respawn", 25f);
    }
    private void InitHealth()
    {
        slider.value = health / totalHealth;
    }
    private void Die()
    {
        animator.SetBool("isDie", true);

        gameObject.SetActive(false);
        SpawnItem();

    }
    private void SpawnItem()
    {
        // Создаем экземпляр предмета
        Instantiate(coinPrefab, transform.position + Vector3.right, Quaternion.identity);

        // Дополнительные действия с предметом, если необходимо
    }

    private void Respawn()
    {
       gameObject.SetActive(true);
        health = totalHealth;
        InitHealth();
    }
}