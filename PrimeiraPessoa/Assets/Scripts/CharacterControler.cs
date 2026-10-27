using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]

public class CharacterControler : MonoBehaviour
{
    [SerializeField] private float WalkSpeed = 5.5f;
    [SerializeField] private float RunningSpeed = 9.0f;
    [SerializeField] private float JumpForce = 8.0f;
    [SerializeField] private float Gravity = 20.0f;
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90f;
    private float lookAngle = 0.0f;
    private float currentMoveSpeed = 0.0f;
    private float verticalVelocity = 0f;
    private Camera mainCamera;
    private CharacterController characterController;
    private InputAction moveInput;
    private InputAction runInput;
    private InputAction jumpInput;
    private InputAction lookInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveInput = InputSystem.actions.FindAction("Move");
        runInput = InputSystem.actions.FindAction("Sprint");
        jumpInput = InputSystem.actions.FindAction("Jump");
        lookInput = InputSystem.actions.FindAction("Look");

        mainCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentMoveSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        currentMoveSpeed = runInput.IsPressed() ? RunningSpeed : WalkSpeed;
        HandleMovement(moveVector);
        Vector2 lookVector = lookInput.ReadValue<Vector2>();
        HandleCamera(lookVector);
    }
    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 moveDirection = (transform.forward * moveVector.y + transform.right * moveVector.x) * currentMoveSpeed;
        if (characterController.isGrounded) // verifica se o player está no chão
        {
            if (jumpInput.WasPressedThisFrame()) // verifica se o player pressionou o pulo
                verticalVelocity = JumpForce;
            else
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity -= Gravity * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    private void HandleCamera(Vector2 lookVector)
    {
        lookAngle += -lookVector.y * LookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -LookAngleLimit, LookAngleLimit);
        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, lookVector.x * LookSensitivity, 0);
    }
}
