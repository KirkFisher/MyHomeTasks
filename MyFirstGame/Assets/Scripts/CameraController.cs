using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    // Update is called once per frame
    void Update()
    {
        _camera.transform.position = new Vector3(transform.position.x,transform.position.y,_camera.transform.position.z);
    }
}
