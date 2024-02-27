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
        _swordCollider.enabled = false; // ��������� BoxCollider2D �� ���������
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

        // �������� ��������� ����
        _swordCollider.enabled = true;

        // �������� ����� FinishAttack ����� 0.5 �������
        Invoke("FinishAttack", 0.5f);
    }

    public void FinishAttack()
    {
        _isAttacking = false;

        // ��������� ��������� ����
        _swordCollider.enabled = false;
    }
} 