using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float targetScale = 5f;   // The target scale you want to reach
    public float duration = 1f;      // The duration over which the scaling occurs

    private float startTime;          // The time when the scaling started

    void OnEnable()
    {
        // Set the initial scale to (0.1, 0.1, 0.1)
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Store the start time when the GameObject is set active
        startTime = Time.time;

        // Call the ScaleObject function every frame
        InvokeRepeating("ScaleObject", 0f, Time.deltaTime);
    }

    void ScaleObject()
    {
        // Calculate the elapsed time since scaling started
        float elapsedTime = Time.time - startTime;

        // Calculate the lerp factor based on elapsed time and duration
        float lerpFactor = Mathf.Clamp01(elapsedTime / duration);

        // Lerp the scale from initialScale to targetScale
        transform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one * targetScale, lerpFactor);

        // If we've reached the target scale, stop invoking the ScaleObject function and deactivate the object
        if (lerpFactor >= 1f)
        {
            CancelInvoke("ScaleObject");
            gameObject.SetActive(false);
        }
    }
}
