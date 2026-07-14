using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    
    [Header("Actions")]
    public InputAction lookAction;
    public InputAction unlockCursorAction;
    public InputAction lockCursorAction;

    [Header("Settings")]
    public float sensitivity = 0.1f;
    public float upperLookLimit = 80f;
    public float lowerLookLimit = -80f;

    // Private Variables
    private float xRotation = 0f;

    void OnEnable()
    {
        // Must enable InputActions when they are defined directly in the script
        lookAction.Enable();
        unlockCursorAction.Enable();
        lockCursorAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
        unlockCursorAction.Disable();
        lockCursorAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 currentRotation = transform.localEulerAngles;
        xRotation = currentRotation.x;
        if (xRotation > 180) xRotation -= 360f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (unlockCursorAction.WasPressedThisFrame())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (lockCursorAction.WasPressedThisFrame() && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

            playerBody.Rotate(Vector3.up * mouseDelta.x * sensitivity);

            xRotation -= mouseDelta.y * sensitivity;
            xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
