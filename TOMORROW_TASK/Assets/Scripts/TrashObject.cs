using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private bool _isAttachedToPlayer = false;
    private Transform _playerTransform;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isAttachedToPlayer && _playerTransform != null)
        {
            // ����������� ������� ������ � ������
            Vector3 targetPosition = _playerTransform.position + _playerTransform.forward * 2.0f; // ������� ����� �������
            _rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10.0f));
        }
    }

    public void AttachToPlayer(Transform playerTransform)
    {
        _isAttachedToPlayer = true;
        _playerTransform = playerTransform;
        _rb.isKinematic = true; // ��������� ������ ��� �������
    }

    public void DetachFromPlayer()
    {
        _isAttachedToPlayer = false;
        _playerTransform = null;
        _rb.isKinematic = false; // �������� ������ ��� �������
    }
}
