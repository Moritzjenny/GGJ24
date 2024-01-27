using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public GameObject happyElements;
    public GameObject sadElements;

    private Animator animator;

    void Awake()
    {
        // Find the Animator component on the same GameObject
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on the same GameObject.");
        }
    }
     

    public void SetHappy()
    {
        happyElements.SetActive(true);
        sadElements.SetActive(false);
        animator.SetBool("happy", true);
    }

    public void SetSad()
    {
        happyElements.SetActive(false);
        sadElements.SetActive(true);
        animator.SetBool("happy", false);
    }
}
