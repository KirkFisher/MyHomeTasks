using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    public Transform[] _parallaxLayers;
    public float[] _parallaxFactors; //Speed factors for images

    private Transform _cameraTransform;
    private Vector3 _previousCameraPosition;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _previousCameraPosition = _cameraTransform.position;
    }

    private void Update()
    {
        Vector3 _parallax = (_previousCameraPosition - _cameraTransform.position)* Time.deltaTime;

        for(int i = 0; i < _parallaxLayers.Length; i++)
        {
            float _parallaxFactor = _parallaxFactors[i];
            Vector3 _layerPosition = _parallaxLayers[i].position;

            _layerPosition.x += _parallax.x * _parallaxFactor;
            _layerPosition.y += _parallax.y * _parallaxFactor;

            _parallaxLayers[i].position = _layerPosition;
        }
        _previousCameraPosition = _cameraTransform.position;
    }
}
