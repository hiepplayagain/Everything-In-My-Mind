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

    bool _isCrouching = false;


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
        _playerCurrentState.UpdateState(this);

        // Get input
        float _inputHorizontal = Input.GetAxisRaw("Horizontal");
        float _inputVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            _isCrouching = !_isCrouching;
            if (_isCrouching)
            {
                if (_currentSpeed > 0.1f) SwitchState(_playerCrouchedWalkState);
                else SwitchState(_playerCrouchedIdleState);

            }
            else
            {
                if (_currentSpeed > 0.1f) SwitchState(_playerWalkState);
                else SwitchState(_playerIdleState);

            }
        }

        // Calculate movement direction in world space
        Vector3 _inputDirection = new Vector3(_inputHorizontal, 0f, _inputVertical).normalized;
        _inputMagnitude = _inputDirection.magnitude;
        //Debug.Log()
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

        _anim.SetFloat("speedMoving", _currentSpeed);
        

    }

    public void SwitchState(PlayerBaseState state)
    {
        _playerCurrentState = state;
        _playerCurrentState.EnterState(this); 
    }
    
}
