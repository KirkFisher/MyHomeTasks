using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    public Transform _player;
    public Vector3 _offset;
    public float rotationSpeed = 5.0f;
    public float zoomSpeed = 2.0f; // �������� ��������� �����������
    public float minZoom = 2.0f;   // ����������� �������� �����������
    public float maxZoom = 10.0f;

    private Vector3 currentRotation;

    void Start()
    {
        if(_offset == Vector3.zero)
        {
            _offset = transform.position - _player.position;
        }

        currentRotation = transform.eulerAngles;
    }
    void LateUpdate()
    {
        // ���������� ������� ������
        transform.position = _player.position + _offset;

        // ������� ������ ��� ����������� ������� ����
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

            currentRotation.y += mouseX;
            currentRotation.x += mouseY;

            currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

            transform.eulerAngles = currentRotation;
        }

        // ����������� � ��������� ������ ��� ��������� ������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float newZoom = _offset.magnitude - scroll;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        _offset = _offset.normalized * newZoom;
    }
}
