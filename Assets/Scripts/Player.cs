using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Movement
{
    [SerializeField] private Camera _camera;
    [Space]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private float _mouseSensitivity = 5;
    [Space]
    [SerializeField] private float _minVerticalAngle = -80f;
    [SerializeField] private float _maxVerticalAngle = 80f;
    private float _verticalLookRotation = 0f;

    private void Start() => Cursor.lockState = CursorLockMode.Locked;

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        if (Mathf.Abs(Look.x) > 0.1f)
            transform.Rotate(0f, Look.x * _mouseSensitivity * Time.fixedDeltaTime, 0f);

        if (Mathf.Abs(Look.y) > 0.1f && _camera != null)
        {
            _verticalLookRotation -= Look.y * _mouseSensitivity * Time.fixedDeltaTime;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, _minVerticalAngle, _maxVerticalAngle);
            _camera.transform.localRotation = Quaternion.Euler(_verticalLookRotation, 0f, 0f);
        }
    }

    private void HandleMovement()
    {
        if (MoveInput == Vector2.zero)
        {
            Rb.linearVelocity = new Vector3(0f, Rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * MoveInput.y + cameraRight * MoveInput.x) * _moveSpeed;
        Rb.linearVelocity = new Vector3(movement.x, Rb.linearVelocity.y, movement.z);
    }

    protected override void Jump(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f) == false)
            return;

        Rb.linearVelocity += _jumpForce * Vector3.up;
    }
}