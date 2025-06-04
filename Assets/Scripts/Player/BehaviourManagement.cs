using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourManagement : MonoBehaviour
{
    Animator _anim;
    CharacterController _characterController;
    CameraController _cameraController;

    [Header("Player stats")]
    public float _speedWalking = 5f;
    public float _speedRunning = 10f;
    //public float _speedRotation = 720f;
    public float _rotationSpeed = 720f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //float _inputVertical = Input.GetAxis("Vertical");
        //float _inputHorizontal = Input.GetAxis("Horizontal");


        //Vector3 _inputMoving = new Vector3(_inputHorizontal, 0f, _inputVertical).normalized;
        ////Debug.Log(_inputMoving);
        //float _inputMovingValue = _inputMoving.magnitude;

        //Quaternion targetRotation;

        //if (_inputMovingValue > 0.1f)
        //{
        //    Vector3 _movingDirection = _cameraController.PlanarRotation * _inputMoving;

        //    _characterController.Move(_movingDirection * _speedWalking * Time.deltaTime);

        //    targetRotation = Quaternion.LookRotation(_movingDirection);

        //}
        //else
        //{
        //    Vector3 cameraForward = _cameraController.PlanarRotation * Vector3.forward;
        //    targetRotation = Quaternion.LookRotation(cameraForward);
        //}

        //transform.rotation = Quaternion.RotateTowards(
        //    transform.rotation,
        //    targetRotation,
        //    _speedRotation * Time.deltaTime 
        //    );

        //_anim.SetFloat("speedWalking", _inputMovingValue);

        // Get input
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        float _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _speedRunning : _speedWalking;

        // Calculate movement direction in world space
        Vector3 inputDirection = new Vector3(inputHorizontal, 0f, inputVertical).normalized;
        float inputMagnitude = inputDirection.magnitude;

        // Only process movement and rotation if there's significant input
        if (inputMagnitude > 0.1f)
        {
            // When moving, face the movement direction
            Vector3 moveDirection = _cameraController.PlanarRotation * inputDirection;
            _characterController.Move(moveDirection * _currentSpeed * Time.deltaTime);

            // Smoothly rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        // Update animator
        _anim.SetFloat("speedWalking", inputMagnitude);

    }

    
}
