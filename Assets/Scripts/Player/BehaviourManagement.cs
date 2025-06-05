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

    #region Moving States
    PlayerBaseState _playerCurrentState;
    public PlayerIdleState _playerIdleState = new PlayerIdleState();
    public PlayerWalkState _playerWalkState = new PlayerWalkState();
    public PlayerRunState _playerRunState = new PlayerRunState();
    public PlayerCrouchState _playerCrouchState = new PlayerCrouchState();
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        SwitchState(_playerIdleState);
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        float _inputHorizontal = Input.GetAxisRaw("Horizontal");
        float _inputVertical = Input.GetAxisRaw("Vertical");
        //float _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _speedRunning : _speedWalking;
        float _currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _speedRunning;
            _anim.SetBool("isRunning", true);
        }
        else
        {
            _currentSpeed = _speedWalking;
            _anim.SetBool("isRunning", false);

        }

        // Calculate movement direction in world space
        Vector3 _inputDirection = new Vector3(_inputHorizontal, 0f, _inputVertical).normalized;
        float _inputMagnitude = _inputDirection.magnitude;

        // Only process movement and rotation if there's significant input
        if (_inputMagnitude > 0.1f)
        {
            // When moving, face the movement direction
            Vector3 moveDirection = _cameraController.PlanarRotation * _inputDirection;
            _characterController.Move(moveDirection * _currentSpeed * Time.deltaTime);

            // Smoothly rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        _anim.SetFloat("speedMoving", _inputMagnitude);
        

    }

    public void SwitchState(PlayerBaseState state)
    {
        _playerCurrentState = state;
        _playerCurrentState.EnterState(this); 
    }
    
}
