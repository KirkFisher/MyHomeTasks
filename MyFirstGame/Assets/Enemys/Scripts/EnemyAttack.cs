using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float timeToDamage = 1f;

    [SerializeField]private Animator _animator;

    private float _damageTime;
    private bool _isDamage;

    private BoxCollider2D _swordCollider;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _damageTime = timeToDamage;

        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = false; // Отключаем BoxCollider2D по умолчанию
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _damageTime -= Time.deltaTime;
            if(_damageTime <= 0)
            {
                _isDamage = true;
                _damageTime = timeToDamage;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamage)
        {
            _animator.SetTrigger("isAttack");
            playerHealth.ReduceHealth(damage);
            _isDamage = false;
            // Запускаем корутину для отключения и включения коллайдера
            //StartCoroutine(DisableEnableCollider());

            EnableSwordCollider();
        }
    }
    public void EnableSwordCollider()
    {
        StartCoroutine(DisableEnableCollider());
    }

    private IEnumerator DisableEnableCollider()
    {
        // Включаем коллайдер
        _swordCollider.enabled = true;

        // Ждем 0.5 секунды
        yield return new WaitForSeconds(3f);

        // Отключаем коллайдер
        _swordCollider.enabled = false;
    }
}