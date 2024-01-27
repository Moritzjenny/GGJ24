using UnityEngine;

public class ChuckyController : MonoBehaviour
{
    private Animator chuckyAnimator;
    private Rigidbody chuckyRigidbody; // Reference to the attached Rigidbody

    void Awake()
    {
        // Find the Animator component on the same GameObject
        chuckyAnimator = GetComponent<Animator>();

        // Find the attached Rigidbody component
        chuckyRigidbody = GetComponent<Rigidbody>();

        if (chuckyRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the same GameObject.");
        }
    }

    public void KickChucky()
    {
        // Check if the Animator component is found
        if (chuckyAnimator != null)
        {
            // Disable the Animator component
            chuckyAnimator.enabled = false;
        }
        else
        {
            Debug.LogError("Animator component not found on the same GameObject.");
        }

        // Check if the Rigidbody component is found
        if (chuckyRigidbody != null)
        {
            // Disable kinematic for the attached Rigidbody
            chuckyRigidbody.isKinematic = false;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the same GameObject.");
        }
    }
}
