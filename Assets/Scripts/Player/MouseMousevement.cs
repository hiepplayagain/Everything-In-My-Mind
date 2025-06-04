using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Range(0f, 100f)]
    public float mouseSensitive;

    float _xRotation = 0f, _yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

        _xRotation -= _mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _yRotation += _mouseX;

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);

    }
}
