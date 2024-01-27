using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    public static ForceController instance;

    public float fillDuration = 1.5f;
    public int maxForce = 10000;

    private Image gradient;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gradient = GetComponentsInChildren<Image>()[1];
        gradient.fillAmount = 0;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            gradient.fillAmount = Math.Clamp(gradient.fillAmount + Time.deltaTime / fillDuration, 0, 1);
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame || gradient.fillAmount == 1)
        {
            Chucky.activeInstance.Kick(gradient.fillAmount * maxForce);
            gradient.fillAmount = 0;
            gameObject.SetActive(false);

        }
    }
}
