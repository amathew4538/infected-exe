using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform playerCamera;

    [Header("Actions")]
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    [Header("Movement Settings")]
    public float moveSpeed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    [Header("Sprint Settings")]
    public float sprintValue = 100.0f;
    public float maxSprint = 100.0f;
    public float sprintDecay = 10.0f;
    public float sprintMult = 2.0f;
    public float sprintPause = 0f;
    public float maxSprintPause = 1.0f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float originalStepOffset;
    private bool isHoldingSprint;

    void OnEnable() 
    { 
        moveAction.Enable(); 
        jumpAction.Enable();
        sprintAction.Enable();
    }
    void OnDisable() 
    { 
        moveAction.Disable(); 
        jumpAction.Disable();
        sprintAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputVector.y) + (right * inputVector.x);

        if (isGrounded)
        {
            controller.stepOffset = originalStepOffset;
        }
        else
        {
            controller.stepOffset = 0f;
        }

        isHoldingSprint = sprintAction.IsPressed();

        if (isHoldingSprint && sprintValue > 0.0f)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime * sprintMult);
            sprintValue = Mathf.Clamp(sprintValue - sprintDecay * Time.deltaTime, 0.0f, maxSprint);
            sprintPause = 0f;
        } 
        else
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            if (!isHoldingSprint)
            {
                if (sprintPause >= maxSprintPause)
                {
                    sprintValue = Mathf.Clamp(sprintValue + sprintDecay * Time.deltaTime, 0.0f, maxSprint);
                }
                else
                {
                    sprintPause += Time.deltaTime;
                }
            }
        }


        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}