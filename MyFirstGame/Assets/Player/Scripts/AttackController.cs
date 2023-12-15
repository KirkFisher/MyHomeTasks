using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    
    private bool _isAttacking;

    public bool isAttack { get => _isAttacking; }

    private BoxCollider2D _swordCollider;

    private void Start()
    {
        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = false; // Отключаем BoxCollider2D по умолчанию
    }
    public void FinishAttack()
    {
        _isAttacking = false;
    }
    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            _isAttacking = true;
            _animator.SetTrigger("IsAttack");
        }*/
    }

    IEnumerator Attack()
    {
        _isAttacking = true;
        _animator.SetTrigger("IsAttack");

        // Код атаки, например, активация коллайдера меча
        _swordCollider.enabled = true;

        // Ждем время анимации атаки
        yield return new WaitForSeconds(0.5f); // Замените на длительность вашей анимации

        // Выключаем состояние атаки
        // _animator.SetTrigger("IsAttack");
        _isAttacking = false;

        // Выключаем коллайдер меча
        _swordCollider.enabled = false;
    }//*/
}
