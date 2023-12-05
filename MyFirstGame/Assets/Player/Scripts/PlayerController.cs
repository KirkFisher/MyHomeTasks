using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    
    public int _health = 10;  
    public Transform _groundCheck;
    public LayerMask _groundLayer;
    

    private int _currentHealth;
    private bool _isDead = false;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isRolling = false;
    private bool _facingRight;
    private bool _isAttacking = false;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _rollSpeed = 10f;// Скорость кувырка
    [SerializeField] private float _rollDuration = 0.5f; // Продолжительность кувырка


    private BoxCollider2D _swordCollider;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _swordCollider = GetComponentInChildren<BoxCollider2D>();
        _swordCollider.enabled = false; // Отключаем BoxCollider2D по умолчанию
        _currentHealth = _health;
    }

    private void Update()
    {
        _isGrounded = _rb.IsTouchingLayers(_groundLayer);
        

        Move();

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Debug.Log("PAW");
            _isJumping = true;
            _animator.SetBool("Jump", true);
            Jump();
        }
        

        if (Input.GetKeyDown(KeyCode.C) && _isGrounded && !_isRolling)
        {
            StartCoroutine(Roll());
        }

        if (Input.GetButtonDown("Fire1") && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        _isAttacking = true;
        _animator.SetBool("IsAttacking", true);

        // Код атаки, например, активация коллайдера меча
        _swordCollider.enabled = true;

        // Ждем время анимации атаки
        yield return new WaitForSeconds(0.5f); // Замените на длительность вашей анимации

        // Выключаем состояние атаки
        _animator.SetBool("IsAttacking", false);
        _isAttacking = false;

        // Выключаем коллайдер меча
        _swordCollider.enabled = false;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 _movement = new Vector2(horizontal * _moveSpeed, _rb.velocity.y);
        _rb.velocity = _movement;

        if((horizontal > 0 && _facingRight )|| horizontal < 0 && !_facingRight)
        {
            Flip();
        }

        if(horizontal != 0)
        {
            _animator.SetFloat("MoveX", Mathf.Abs(horizontal));
        }
        else
        {
            _animator.SetFloat("MoveX", 0f);
        }
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("PEW");
        // Обработка касания земли после прыжка
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private IEnumerator Roll()
    {
        _isRolling = true;
        _animator.SetBool("Rolling", true);
        float rollTimer = 0f;

        while (rollTimer < _rollDuration)
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 rollMovement = new Vector2(horizontal * _rollSpeed, _rb.velocity.y);
            _rb.velocity = rollMovement;

            if ((horizontal > 0 && _facingRight) || (horizontal < 0 && !_facingRight))
            {
                Flip();
            }

            rollTimer += Time.deltaTime;
            yield return null;
        }

        _isRolling = false;
        _animator.SetBool("Rolling", false);
    }
}
