using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chucky : MonoBehaviour
{
    public static Chucky activeInstance;
    public bool happy;

    private new Rigidbody rigidbody;
    private ChuckyController chuckyController;
    private EmotionController emotionController;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        chuckyController = GetComponent<ChuckyController>();
        emotionController = GetComponent<EmotionController>();
    }

    private void Start()
    {
        if (happy)
        {
            GameManager.instance.IncrementHappyChuckies();
            emotionController.SetHappy();
            CheckContagion();
        }
        else
        {
            emotionController.SetSad();
        }
    }

    private void Update()
    {
        bool leftButtonClick = Mouse.current.leftButton.wasPressedThisFrame;
        bool rightButtonClick = Mouse.current.rightButton.wasPressedThisFrame;

        if (leftButtonClick || rightButtonClick)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue, 2f); // Draw a debug ray

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.transform.IsChildOf(transform))
            {
                if (leftButtonClick)
                {
                    if (activeInstance == this)
                    {
                        activeInstance = null;
                        CameraController.instance.ResetPivot();
                        ForceController.instance.gameObject.SetActive(false);
                    }
                    else
                    {
                        activeInstance = this;
                        CameraController.instance.SetPivot(transform);
                        ForceController.instance.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void Kick(float force)
    {
        // Inform ChuckyController about kick, animation and gamelogic affected by the impulse is going to be handled there
        chuckyController.KickChucky();
        rigidbody.AddForce(force * CameraController.instance.transform.forward);
        chuckyController.CheckForRecovery();

        StartCoroutine(InvokeAfterSeconds(CameraController.instance.resetDelay, Reset));
    }

    IEnumerator InvokeAfterSeconds(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback.Invoke();
    }

    private void Reset()
    {
        activeInstance = null;
        CameraController.instance.ResetPivot();
    }

    public void SetHappy()
    {
        if (happy)
        {
            // nothing to do if already happy
            return;
        };

        happy = true;
        GameManager.instance.IncrementHappyChuckies();
        emotionController.SetHappy();
        CheckContagion();
    }

    public void CheckContagion()
    {
        foreach (var chucky in GameManager.instance.chuckies)
        {
            if (chucky == this)
            {
                // can't be contagious to itself
                continue;
            }

            Vector3 direction = transform.position - chucky.transform.position;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Obstacle")))
            {
                // nothing to do if obstacle is hit
                continue;
            }

            if (Vector3.Distance(transform.position, chucky.transform.position) <= GameManager.instance.contagionDistance && (happy || chucky.happy))
            {
                // set both happy if close enough
                SetHappy();
                chucky.SetHappy();
            }
        }
    }
}