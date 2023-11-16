using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontal;
    private bool _isGround;
    private bool _isJump;
    [SerializeField]private float _speedX = 2f;
    private float _speedMulti = 50f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Jump();
        
    }


    

    private void Movement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_horizontal * _speedX * _speedMulti * Time.deltaTime, _rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && _isGround)
        {
            _isJump = true;
            if (_isJump)
            {
                _rb.AddForce(new Vector2(0f, 250f));
                _isGround = false;
                _isJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }
}
