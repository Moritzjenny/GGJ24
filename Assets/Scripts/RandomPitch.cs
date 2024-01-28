using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchAndStart : MonoBehaviour
{
    public float pitchRange = 0.1f; // The range of pitch variation
    public bool changeStartPoint = true;
    public bool deactivateOnFinish = false;

    private float originalPitch; // Store the original pitch

    void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        // Check if an AudioSource component is attached
        if (audioSource != null)
        {
            // Store the original pitch when the object is first enabled
            originalPitch = audioSource.pitch;

            // Generate a random pitch within the specified range
            float randomPitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);

            // Set the random pitch to the AudioSource
            audioSource.pitch = randomPitch;

            // Set the time to start playing at a random point within the audio clip
            if (changeStartPoint)
            {
                audioSource.time = Random.Range(0f, audioSource.clip.length);
            }

            // Play the audio
            audioSource.Play();

            // Check if the GameObject should be deactivated when the sound finishes playing
            if (deactivateOnFinish)
            {
                // Invoke the DeactivateGameObject method after the audio clip duration
                Invoke("DeactivateGameObject", audioSource.clip.length);
            }
        }
        else
        {
            Debug.LogWarning("No AudioSource component found on the GameObject.");
        }
    }

    // Method to deactivate the GameObject
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    // Optionally, you can add an OnDisable method to reset the pitch when the object is disabled
    void OnDisable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.pitch = originalPitch;
        }
    }
}
