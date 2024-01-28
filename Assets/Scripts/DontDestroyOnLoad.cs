using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Check if there's another instance of this script attached to a GameObject
        DontDestroyOnLoad(this.gameObject);
    }
}
