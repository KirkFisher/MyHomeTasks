using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    public Camera Cam;

    private void LateUpdate()
    {
        transform.forward = Cam.transform.forward;
    }
}
