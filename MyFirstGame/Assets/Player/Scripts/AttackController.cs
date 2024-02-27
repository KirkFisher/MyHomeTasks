using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private PlayerInventory _playerInventory;


    private bool _isAttacking;

    public bool isAttack { get => _isAttacking; }

    private BoxCollider2D _swordCollider;

    private void Start()
    {
        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = false; // Отключаем BoxCollider2D по умолчанию
    }

    private void Update()
    {
        bool inventoryActive = _playerInventory.isOpen; 

        if (Input.GetButtonDown("Fire1") && !_isAttacking && !inventoryActive)
        {
            attackSound.Play();
            Attack();
        }
    }

    private void Attack()
    {
        _isAttacking = true;
        _animator.SetTrigger("IsAttack");

        // Включаем коллайдер меча
        _swordCollider.enabled = true;

        // Вызываем метод FinishAttack через 0.5 секунды
        Invoke("FinishAttack", 0.5f);
    }

    public void FinishAttack()
    {
        _isAttacking = false;

        // Выключаем коллайдер меча
        _swordCollider.enabled = false;
    }
} 