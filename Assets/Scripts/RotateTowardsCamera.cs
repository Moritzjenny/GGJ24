using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // Find the main camera's transform
        cameraTransform = Camera.main.transform;

        if (cameraTransform == null)
        {
            Debug.LogError("Main camera not found. Make sure you have a camera in the scene tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        // Calculate the vector from the camera to the object
        Vector3 toObject = transform.position - cameraTransform.position;

        // Project the vector onto a flat plane (ignore the vertical component)
        Vector3 toObjectProjected = Vector3.ProjectOnPlane(toObject, Vector3.up);

        // Ensure the vector is not zero before rotating
        if (toObjectProjected.sqrMagnitude > 0f)
        {
            // Calculate the rotation to face the camera after projection
            Quaternion targetRotation = Quaternion.LookRotation(toObjectProjected);

            // Apply the rotation
            transform.rotation = targetRotation;
        }
    }
}
