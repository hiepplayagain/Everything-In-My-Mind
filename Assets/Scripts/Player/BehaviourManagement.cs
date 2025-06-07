using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviourManagement : MonoBehaviour
{
    public Animator _anim;
    CharacterController _characterController;
    CameraController _cameraController;

    [Header("Player stats")]
    public float _inputMagnitude;
    public float _currentSpeed;
    public float _speedWalking = 5f;
    public float _speedRunning = 10f;
    //public float _speedRotation = 720f;
    public float _rotationSpeed = 720f;

    #region Moving States
    PlayerBaseState _playerCurrentState;
    public PlayerIdleState _playerIdleState = new();
    public PlayerWalkState _playerWalkState = new();
    public PlayerRunState _playerRunState = new();
    public PlayerCrouchedIdleState _playerCrouchedIdleState = new();
    public PlayerCrouchWalkState _playerCrouchedWalkState = new();
    #endregion

    public bool _isCrouching = false;

    void Start()
    {
        SwitchState(_playerIdleState);
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        _playerCurrentState.UpdateState(this);

        Moving();

    }

    public void Moving()
    {
        
        float _inputHorizontal = Input.GetAxisRaw("Horizontal");
        float _inputVertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction in world space
        Vector3 _inputDirection = new Vector3(_inputHorizontal, 0f, _inputVertical).normalized;
        _inputMagnitude = _inputDirection.magnitude;
        
        
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

        _anim.SetFloat("speedMoving", _currentSpeed);

    }

    public void SwitchState(PlayerBaseState state)
    {
        _playerCurrentState = state;
        _playerCurrentState.EnterState(this); 
    }
    
}
