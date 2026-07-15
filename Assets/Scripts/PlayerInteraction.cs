using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Actions")]
    public InputAction interactAction;
    [Header("Settings")]
    public float interactionRange = 3.0f;

    void Update()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            print("Hit Something");
        } else
        {
            print("Missed");
        }
    }
}