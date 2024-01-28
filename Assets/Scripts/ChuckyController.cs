using System.Collections;
using UnityEngine;

public class ChuckyController : MonoBehaviour
{
    private Animator chuckyAnimator;
    private Rigidbody chuckyRigidbody; // Reference to the attached Rigidbody
    private EmotionController emotionController;
    private Chucky chucky;
    public GameObject bumpSound;

    public GameObject redCircle;
    public GameObject greenCircle;

    private Vector3 originalPosition; // Store the original position before kick


    void Awake()
    {
        emotionController = GetComponent<EmotionController>();
        // Find the Animator component on the same GameObject
        chuckyAnimator = GetComponent<Animator>();

        // Find the attached Rigidbody component
        chuckyRigidbody = GetComponent<Rigidbody>();

        // Get Chucky
        chucky = GetComponent<Chucky>();

        if (chuckyRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on the same GameObject.");
        }
    }

    public void KickChucky()
    {
        // Store the original position before kick
        originalPosition = transform.position;

        // Disable the Animator component
        chuckyAnimator.enabled = false;

        // Disable kinematic for the attached Rigidbody
        chuckyRigidbody.isKinematic = false;

        // Set sad while flying
        emotionController.SetSad();

        // Play the bumpsound
        bumpSound.SetActive(true);
    }

    public void CheckForRecovery()
    {
        // Start Coroutine to continuously check velocity and collision until it's safe to recover
        StartCoroutine(CheckVelocityAndCollisionCoroutine());
    }

    private IEnumerator CheckVelocityAndCollisionCoroutine()
    {
        Collider chuckyCollider = GetComponent<Collider>();

        yield return new WaitForFixedUpdate();

        // Wait while the velocity is greater than or equal to 0.1f or is still colliding with "Obstacle"
        while (chuckyRigidbody.velocity.magnitude >= 0.8f || IsCollidingWithTag(chuckyCollider, "Obstacle"))
        {
            yield return new WaitForFixedUpdate();
        }

        // Velocity has fallen below 0.1 and is not colliding with 'Obstacle', start the GetUpCoroutine
        StartCoroutine(GetUpCoroutine(0.8f));
    }


    private bool IsCollidingWithTag(Collider collider, string tag)
    {
        // Check if the collider is colliding with an object with the specified tag
        Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    public void ActivateRedCircle()
    {
        redCircle.SetActive(true);
    }

    public void DeactivateRedCircle()
    {
        redCircle.SetActive(false);
    }

    public void ActivateGreenCircle()
    {
        greenCircle.SetActive(true);
    }

    public void DeactivateGreenCircle()
    {
        greenCircle.SetActive(false);
    }



    private IEnumerator GetUpCoroutine(float duration)
    {
        // Store the original local forward vector before the turn
        Vector3 originalForward = transform.forward;

        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.identity;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        transform.localRotation = targetRotation;

        // Calculate the direction of the local z vector before the turn
        Vector3 originalForwardProjected = Vector3.ProjectOnPlane(originalForward, Vector3.up).normalized;

        // Calculate the angle to rotate the object around the y-axis (multiplying by -1 to reverse the direction)
        float angleToRotate = -Vector3.SignedAngle(originalForwardProjected, transform.forward, Vector3.up);

        // Rotate the object to align its local z vector with the original forward direction (projected on a horizontal plane)
        transform.Rotate(Vector3.up, angleToRotate, Space.Self);

        // Trigger the Recover method after GetUpCoroutine has completed
        Recover();
    }

    public void Recover()
    {
        // Enable kinematic for the attached Rigidbody
        chuckyRigidbody.isKinematic = true;

        // Enable the Animator component
        chuckyAnimator.enabled = true;

        // Set happy after recovering
        if (chucky.happy)
        {
            emotionController.SetHappy();
            greenCircle.SetActive(true);
        }

        chucky.CheckContagion();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the "OutOfBounds" trigger collider
        if (other.CompareTag("OutOfBounds"))
        {
            // Reset the Chucky's position to the original stored position before kick
            transform.position = originalPosition;
            Recover();
        }
    }
}
