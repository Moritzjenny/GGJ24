using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckyController : MonoBehaviour
{
    Animator chuckyAnimator;

    void Awake(){
        // Find the Animator component on the same GameObject
        chuckyAnimator = GetComponent<Animator>();
    }

    public void KickChucky()
    {
        print("kick");
        // Check if the Animator component is found
        if (chuckyAnimator != null)
        {
            // Disable the Animator component
            chuckyAnimator.enabled = false;
        }
        else
        {
            Debug.LogError("Animator component not found on the same GameObject.");
        }
    }
}
