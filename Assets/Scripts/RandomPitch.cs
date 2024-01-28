using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitch : MonoBehaviour
{
    public float pitchRange = 0.1f; // The range of pitch variation

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        // Check if an AudioSource component is attached
        if (audioSource != null)
        {
            // Get the current pitch
            float originalPitch = audioSource.pitch;

            // Generate a random pitch within the specified range
            float randomPitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);

            // Set the random pitch to the AudioSource
            audioSource.pitch = randomPitch;

            // Set the time to start playing at a random point within the audio clip
            audioSource.time = Random.Range(0f, audioSource.clip.length);

            // Loop the audio
            audioSource.loop = true;

            // Play the audio
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource component found on the GameObject.");
        }
    }
}
