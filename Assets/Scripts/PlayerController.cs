using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public InputAction movementAction;
    public InputAction jumpAction;
    public InputAction sprintAction;

    [Header("Settings")]
    public float walkSpeed = 1.0f;
    public float sprintSpeed = 2.0f;

    public void OnEnable()
    {
        movementAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }

    public void OnDisable()
    {
        movementAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = movementAction.ReadValue<Vector2>();

        if (sprintAction.IsPressed()){
            transform.position += (Vector3)moveInput * walkSpeed * Time.deltaTime * sprintSpeed;
        } else
        {
            transform.position += (Vector3)moveInput * walkSpeed * Time.deltaTime;
        }
    }
}
