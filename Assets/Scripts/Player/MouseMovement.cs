using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform _followTarget;

    [Tooltip("Change the sensitivity of the mouse")]
    [Range(50f, 150f)]
    public float _mouseSensitive;

    [Tooltip("Distance from camera to player")]
    [Range(1f, 5f)]
    public float _cameraDistance;

    [SerializeField] float _minVerticalAngle = -45f;
    [SerializeField] float _maxVerticalAngle = 45f;
    [SerializeField] Vector2 _framingOffset;

    float _rotationX = 0f, _rotationY = 0f;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraDistance = 5f;
    }

    private void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X") * _mouseSensitive * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitive * Time.deltaTime;
        float _mouseCenter = Input.GetAxis("Mouse ScrollWheel");

        _cameraDistance += _mouseCenter;
        _cameraDistance = Mathf.Clamp(_cameraDistance, 1f, 5f);

        _rotationX += _mouseY;
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);
        _rotationY += _mouseX;
        

        var _targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        var _focusPosition = _followTarget.position + new Vector3(_framingOffset.x, _framingOffset.y);

        transform.SetPositionAndRotation (_focusPosition - _targetRotation * new Vector3(0, 0, _cameraDistance), _targetRotation);

    }

    public Quaternion PlanarRotation => Quaternion.Euler(0f, _rotationY, 0f);

}
