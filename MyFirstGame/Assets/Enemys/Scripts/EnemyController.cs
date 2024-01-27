using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _walkDistance = 6f;
    [SerializeField] private float _patrolSpeed = 0.9f;
    [SerializeField] private float _chasinfSpeed = 3f; 
    [SerializeField] private float _timeToWait = 5f;
    [SerializeField] private float minDistanseToPlayer = 1.5f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private Transform enemyModelTransform;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _nextPoint;
    private Vector2 _positionA;
    private Vector2 _positionB;
    private Animator _animator;
    private EnemyAttack _enemyAttack;

    private bool _isChaisingPlayer;
    private bool _isFacingRight = true;
    private bool _isWait = false;  //Для ожидания на точках

    private float _waitTime;  //Время ожидания
    private float _chaseTime;
    private float _walkSpeed; 


    public bool IsFacingRight //для доступа приватной данной в других скриптах
    {
        get => _isFacingRight;
    }

    public void StartChaisingPlayer()
    {
        _isChaisingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = _chasinfSpeed;
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _positionA = transform.position;
        _positionB = _positionA + Vector2.right * _walkDistance;
        _waitTime = _timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = _patrolSpeed;

        _animator = GetComponent<Animator>();

        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        if (_isChaisingPlayer)
        {
            StartChasingTimer();
        }
        if (_isWait && !_isChaisingPlayer)
        {
            StartWaitTimer();
        }

        
        if (ShouldWait())
        {

            _animator.SetBool("Movement", false);
            _isWait = true;
        }
    }

    private void FixedUpdate()
    {
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChaisingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanseToPlayer) return;
        
        if (_isChaisingPlayer)
        {
            ChasePlayer();
            if (_enemyAttack != null)
            {
                _enemyAttack.EnableSwordCollider();
            }
        }

        if (!_isWait && !_isChaisingPlayer)
        {
            Patrol();
        }
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;

        if( _chaseTime < 0f)
        {
            _isChaisingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = _patrolSpeed;
        }
    }
    private bool ShouldWait()
    {
        bool isOutOfRightPos = _isFacingRight && transform.position.x >= _positionB.x;
        bool isOutOfLeftPos = !_isFacingRight && transform.position.x <= _positionA.x;

        return isOutOfLeftPos || isOutOfRightPos;
    }
    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }
    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();

        if(distance < 0)
        {
            _nextPoint.x *= -1;
        }

        if(distance > 0.2f && !_isFacingRight)
        {
            Flip();
        } else if(distance < 0.2f && _isFacingRight) { 
            Flip(); 
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void StartWaitTimer()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = _timeToWait;
            _isWait = false;
            Flip();
        }
    }
    private void Patrol()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }

        _rb.MovePosition((Vector2)transform.position + _nextPoint);
        _animator.SetBool("Movement", true);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = enemyModelTransform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_positionA, _positionB);
    }
}
