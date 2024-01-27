using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public GameObject happyElements;
    public GameObject sadElements;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
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
