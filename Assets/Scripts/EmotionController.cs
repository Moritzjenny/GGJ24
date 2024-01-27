using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public GameObject happyElements;
    public GameObject sadElements;

    public void SetHappy()
    {
        happyElements.SetActive(true);
        sadElements.SetActive(false);
    }

    public void SetSad()
    {
        happyElements.SetActive(false);
        sadElements.SetActive(true);
    }
}
