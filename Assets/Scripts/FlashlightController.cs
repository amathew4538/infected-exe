using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Transform playerTransform;

    [Header("Actions")]
    public InputAction toggleFlashlight;

    [Header("Settings")]
    public float smoothSpeed = 5f;

    private Light light;

    void OnEnable()
    {
        toggleFlashlight.Enable();
    }

    void OnDisable()
    {
        toggleFlashlight.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleFlashlight.WasPressedThisFrame())
        {
            light.enabled = !light.enabled;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.position = cameraTransform.position;

            float xDegrees = cameraTransform.localEulerAngles.x;
            float yDegrees = playerTransform.eulerAngles.y;

            Quaternion targetRotation = Quaternion.Euler(xDegrees, yDegrees, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }
}
