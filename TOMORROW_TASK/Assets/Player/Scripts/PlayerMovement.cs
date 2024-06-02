using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject _groundObject;

    private Rigidbody _rb;
    private Vector3 _previousRotation;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _sphereSpeed = 2.0f; // ����������� �������� �����
    [SerializeField] private float _robotSpeed = 1.0f;  // ������� �������� ������
    [SerializeField] private float _rotationSpeed = 100.0f;

    private bool _isSphereMode = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundObject = GameObject.FindWithTag("Ground");
        _previousRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ������������ ������� �� ������ ������ ����
        {
            ToggleMode();
        }

        if (_isGrounded)
        {
            HandleMovement();
        }

        CheckRotation();
    }

    private void HandleMovement()
    {
        float speed = _isSphereMode ? _sphereSpeed : _robotSpeed;


        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");


        Vector3 movementDirection = new Vector3(horizontalMove, 0.0f, verticalMove).normalized;


        _rb.AddForce(movementDirection * speed, ForceMode.Force);


        if (_isSphereMode && horizontalMove != 0)
        {
            transform.Rotate(0f, horizontalMove * _rotationSpeed * Time.deltaTime, 0f);
        }
    }

    private void CheckRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        if (currentRotation != _previousRotation)
        {
            Debug.Log("����� ����������");
            _previousRotation = currentRotation;
        }
    }

    private void ToggleMode()
    {
        _isSphereMode = !_isSphereMode;

        if (_isSphereMode)
        {
            Debug.Log("����� ����� �����������");
            _rb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            Debug.Log("����� ������ �����������");
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _groundObject)
        {
            _isGrounded = true;
            Debug.Log("�� �����");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _groundObject)
        {
            _isGrounded = false;
            Debug.Log("�� �� �����");
        }
    }
}