using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] Transform _followTarget;

    [Header("Mouse Settings")]
    [Tooltip("Độ nhạy chuột")]
    [Range(50f, 150f)]
    public float _mouseSensitive = 100f;

    [Header("Camera Settings")]
    [Tooltip("Khoảng cách từ camera đến player")]
    [Range(1f, 5f)]
    public float _cameraDistance = 3f;

    [Tooltip("Góc nhìn tối thiểu (nhìn xuống)")]
    [SerializeField] float _minVerticalAngle = -30f;

    [Tooltip("Góc nhìn tối đa (nhìn lên)")]
    [SerializeField] float _maxVerticalAngle = 60f;

    [Header("Obstacle Detection")]
    [Tooltip("Layer mask cho các vật cản")]
    public LayerMask obstacleMask = -1;

    [Tooltip("Khoảng cách tối thiểu từ vật cản")]
    [SerializeField] float _obstacleBuffer = 0.2f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    [Header("Smooth Settings")]
    [SerializeField] float _smoothSpeed = 10f;
    [SerializeField] bool _enableSmoothing = true;

    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        if (_followTarget == null)
        {
            Debug.LogError("Follow Target chưa được gán cho Camera Controller!");
        }
    }

    private void LateUpdate()
    {
        if (_followTarget == null) return;

        HandleMouseInput();
        CalculateCameraPosition();
    }

    private void HandleMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitive * Time.deltaTime;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        _cameraDistance -= scrollInput * 2f; 
        _cameraDistance = Mathf.Clamp(_cameraDistance, 1f, 5f);

        _rotationY += mouseX; 
        _rotationX -= mouseY; 

        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);
    }

    private void CalculateCameraPosition()
    {
        Quaternion targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

        Vector3 desiredCameraPosition = _followTarget.position - targetRotation * Vector3.forward * _cameraDistance;

        Vector3 finalCameraPosition = CheckForObstacles(_followTarget.position, desiredCameraPosition);

        if (_enableSmoothing)
        {
            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, finalCameraPosition,
                                            _smoothSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                                _smoothSpeed * Time.deltaTime);
        }
        else
        {
            // Instant movement
            transform.position = finalCameraPosition;
            transform.rotation = targetRotation;
        }

        // Debug visualization
        Debug.DrawLine(transform.position, _followTarget.position, Color.red);
        Debug.DrawRay(_followTarget.position, targetRotation * Vector3.forward * _cameraDistance, Color.blue);
    }

    private Vector3 CheckForObstacles(Vector3 focusPos, Vector3 desiredPos)
    {
        
        Vector3 directionToCamera = desiredPos - focusPos;
        float distanceToCamera = directionToCamera.magnitude;

        
        if (Physics.Raycast(focusPos, directionToCamera.normalized, out RaycastHit hit, distanceToCamera, obstacleMask))
        {
            
            Vector3 hitPosition = hit.point - directionToCamera.normalized * _obstacleBuffer;
            return hitPosition;
        }

        
        return desiredPos;
    }

    
    public Quaternion PlanarRotation => Quaternion.Euler(0f, _rotationY, 0f);

    
    public void ResetCamera()
    {
        if (_followTarget != null)
        {
            _rotationY = _followTarget.eulerAngles.y;
            _rotationX = 0f;
        }
    }

    
    public void SetTarget(Transform newTarget)
    {
        _followTarget = newTarget;
    }
}