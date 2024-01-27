using System.Collections;
using UnityEngine;

public class ChuckyController : MonoBehaviour
{
    private Animator chuckyAnimator;
    private Rigidbody chuckyRigidbody; // Reference to the attached Rigidbody
    private EmotionController emotionController;

    void Awake()
    {
        emotionController = GetComponent<EmotionController>();
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
        // Disable the Animator component
        chuckyAnimator.enabled = false;

        // Disable kinematic for the attached Rigidbody
        chuckyRigidbody.isKinematic = false;

        // Set sad while flying
        emotionController.SetSad();

        // Start a coroutine to recover after 1 second
        StartCoroutine(RecoverAfterDelayCoroutine(1f));
    }

    private IEnumerator RecoverAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        GetUp();
    }

    public void Recover(){
         // Enable kinematic for the attached Rigidbody
        chuckyRigidbody.isKinematic = true;

        // Enable the Animator component
        chuckyAnimator.enabled = true;

        // Set happy after recovering
        emotionController.SetHappy();
    }

private IEnumerator GetUpCoroutine(float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.identity; // Upright position (rotation 0, 0, 0)

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;
        Recover();
    }

    public void GetUp()
    {
        // Start the GetUpCoroutine with a duration of 1 second
        StartCoroutine(GetUpCoroutine(1f));
    }
}
