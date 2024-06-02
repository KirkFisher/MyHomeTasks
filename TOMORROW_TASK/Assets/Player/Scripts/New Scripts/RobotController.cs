using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float _sphereSpeed = 4f;
    public float _robotSpeed = 2f;
    private float _currentSpeed;

    private bool _isClosedMode = true;
    private Vector2 _moveInput;

    private Rigidbody _rb;
    public GameObject _radiusObj;

    public Vector2 MoveInput
    {
        get { return _moveInput; }
        set { _moveInput = value; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _currentSpeed = _sphereSpeed;
    }

    private void FixedUpdate()
    {
        if (_isClosedMode)
        {
            Roll();
        }
        else
        {
            Move();
        }
    }
    private void Roll()
    {
        Vector3 force = new Vector3(_moveInput.x, 0, _moveInput.y) * _currentSpeed;
        _rb.AddForce(force);
    }

    private void Move()
    {
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y) * _currentSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + movement);
    }

    public void SwitchMode()
    {
        _isClosedMode = !_isClosedMode;
        _currentSpeed = _isClosedMode ? _sphereSpeed : _robotSpeed;

        // Активируем или деактивируем объект RadiusObj в зависимости от режима
        _radiusObj.SetActive(!_isClosedMode);

        // Меняем физику при переключении режима
        _rb.constraints = _isClosedMode ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
        Debug.Log("Switched to " + (_isClosedMode ? "Sphere Mode" : "Robot Mode"));
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        // Логика взаимодействия с ресурсами
    }

    public void Attack()
    {
        Debug.Log("Attacking");
        // Логика атаки
    }

    public void Dodge()
    {
        Debug.Log("Dodging");
        // Логика уклонения
    }
}
