using UnityEngine;

public class RestartParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // Get the ParticleSystem component attached to this GameObject
        particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem == null)
        {
            Debug.LogError("Particle System component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Check if the ParticleSystem is not currently playing
        if (!particleSystem.isPlaying)
        {
            // Restart the ParticleSystem after a random delay between 1 and 3 seconds
            float randomDelay = Random.Range(1f, 3f);
            Invoke("Restart", randomDelay);
        }
    }

    void Restart()
    {
        // Clear the current particles
        particleSystem.Clear();

        // Play the ParticleSystem again
        particleSystem.Play();
    }
}
