using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTEnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float timeToDamage = 1f;
    [SerializeField] private AudioSource enemyHittingSound;
    [SerializeField] private Animator _animator;

    private float _damageTime;
    private bool _isDamage;

    private BoxCollider2D _swordCollider;
    private Coroutine damageCoroutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _damageTime = timeToDamage;

        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = true; // Вкл BoxCollider2D по умолчанию
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (collision.CompareTag("Player") && playerHealth != null)
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageRepeatedly(playerHealth));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            _isDamage = false; // Деактивация атаки до следующего входа
        }
    }

    // Корутина для нанесения урона постоянно с определенным интервалом времени
    private IEnumerator DealDamageRepeatedly(PlayerHealth playerHealth)
    {
        while (true)
        {
            if (!_isDamage)
            {
                _isDamage = true;
                _animator.SetTrigger("isAttack");
                enemyHittingSound.Play();
                playerHealth.ReduceHealth(damage);
                yield return new WaitForSeconds(timeToDamage);
                _isDamage = false;
            }
            yield return null;
        }
    }
}