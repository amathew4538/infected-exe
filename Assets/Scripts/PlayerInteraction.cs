using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void Update()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 3.0f))
        {
            print("Hit Something");
        } else
        {
            print("Missed");
        }
    }
}