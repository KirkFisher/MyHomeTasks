using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    public Transform _groundCheck;
    public LayerMask _groundLayer;

    
    private Finish _finish;
    private LeverArm _arm;
    
    private bool _isFinish;
    private bool _isDead = false;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isRolling = false;
    private bool _facingRight;
    private bool isLeverArm = false;
    private int Coins = CoinsText.Coins;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _rollSpeed = 10f;// Скорость кувырка
    [SerializeField] private float _rollDuration = 0.5f; // Продолжительность кувырка
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private float interactionDistance = 2f; // Adjust this value as needed
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private PlayerInventory _playerInventory;


    public Animator _animator;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _arm = FindObjectOfType<LeverArm>();
    }

    private void Update()
    {
        _isGrounded = _rb.IsTouchingLayers(_groundLayer);
        

        Move();

        bool inventoryActive = _playerInventory.isOpen;
        if (Input.GetButtonDown("Jump") && _isGrounded && !inventoryActive)
        {
            Debug.Log("Jump!");
            _isJumping = true;
            jumpSound.Play();
            _animator.SetBool("Jump", true);
            Jump();
        }
        

        if (Input.GetKeyDown(KeyCode.C) && _isGrounded && !_isRolling)
        {
            StartCoroutine(Roll());
        }

        Finish();

        StartDialogTrigger();
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
        
        // Обработка касания земли после прыжка
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Coins"))
        {
            CoinsText.Coins += 1;
            Destroy(gameObject);
        }

    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = playerModelTransform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void StartDialogTrigger()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactionDistance, interactionLayerMask);
            if (hit.collider != null)
            {
                NPCController npc = hit.collider.GetComponent<NPCController>();
                if (npc != null)
                {
                    npc.TriggerDialog();
                }
            }
        }
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

            if ((horizontal > 0 && !_facingRight) || (horizontal < 0 && _facingRight))
            {
                Flip();
            }

            rollTimer += Time.deltaTime;
            yield return null;
        }

        _isRolling = false;
        _animator.SetBool("Rolling", false);
    }

    private void Finish()
    {
        if(Input.GetKeyDown(KeyCode.F) && _isFinish)
        {
            _finish.FinishLevel();
        }
        if(Input.GetKeyDown(KeyCode.F) && isLeverArm)
        {
            _arm.ActivateLeverArm();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeverArm leverArm = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Finish");
            _isFinish = true;
        }
        if (leverArm != null)
        {
            isLeverArm = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LeverArm leverArm = collision.GetComponent<LeverArm>();
        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Finish exit");
            _isFinish = false;
        }
        if (leverArm = null)
        {
            isLeverArm = false;
        }
    }
}
