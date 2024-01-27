using UnityEngine;

public class DebugRayDistance : MonoBehaviour
{
    public float rayDistance = 10f; // Adjust this value to set the distance of the ray

    void Update()
    {
        // Draw a ray in the forward direction of the GameObject
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
    }
}
