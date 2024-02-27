using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float timeToDamage = 1f;
    [SerializeField] private AudioSource enemyHittingSound;
    [SerializeField] private Animator _animator;

    private float _damageTime;
    private bool _isDamage;

    private BoxCollider2D _swordCollider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _damageTime = timeToDamage;

        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = false; // ��������� BoxCollider2D �� ���������
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _damageTime -= Time.deltaTime;
            if (_damageTime <= 0)
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
            enemyHittingSound.Play();
            playerHealth.ReduceHealth(damage);
            _isDamage = false;
            Debug.Log("1");
            // �������� ��������� �� �������� �����
            EnableSwordCollider();

            // �������� ����� ��� ���������� ���������� ����� 1 �������
            Invoke("DisableSwordCollider", 1f);

            // ���������� ������ �����
            _damageTime = timeToDamage;
        }
    }

    public void EnableSwordCollider()
    {
        // �������� ���������
        _swordCollider.enabled = true;
    }

    private void DisableSwordCollider()
    {
        // ��������� ���������
        _swordCollider.enabled = false;
    }
}