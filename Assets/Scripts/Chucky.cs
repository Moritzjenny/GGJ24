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
        chuckyController.DeactivateRedCircle();
    }

    private void Update()
    {
        if (activeInstance == null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue, 2f); // Draw a debug ray

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.transform.IsChildOf(transform))
            {
                chuckyController.ActivateRedCircle();
                activeInstance = this;
                CameraController.instance.SetPivot(transform);
                ForceController.instance.gameObject.SetActive(true);
            }
        }
    }

    public void Kick(float force)
    {
        activeInstance = null;
        chuckyController.DeactivateRedCircle();
        // Inform ChuckyController about kick, animation and gamelogic affected by the impulse is going to be handled there
        chuckyController.KickChucky();
        rigidbody.AddForce(force * CameraController.instance.transform.forward);
        chuckyController.CheckForRecovery();
        GameManager.instance.IncrementBumps();

        StartCoroutine(ResetCamera());
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(CameraController.instance.resetDelay);
        CameraController.instance.ResetPivot();
    }

    public void SetHappy()
    {
        if (happy)
        {
            // nothing to do if already happy
            return;
        };


        chuckyController.ActivateGreenCircle();
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

            var origin = transform.position + Vector3.up;
            var direction = (chucky.transform.position + Vector3.up - origin).normalized;
            var layerMask = LayerMask.GetMask("Obstacle", "Chucky");
            Debug.DrawRay(origin, direction * GameManager.instance.contagionDistance, Color.red, 10f); // Draw a debug ray
            if (!Physics.Raycast(origin, direction, out RaycastHit hit, GameManager.instance.contagionDistance, layerMask))
            {
                // nothing to do if nothing was hit
                continue;
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                // do nothing if obstacle was hit
                continue;
            }

            if (hit.collider.GetComponent<Chucky>() == chucky && (happy || chucky.happy))
            {

                // set both happy after a delay
                StartCoroutine(SetBothHappy(this, chucky));
            }
        }
    }

    private IEnumerator SetBothHappy(Chucky chucky, Chucky otherChucky)
    {
        yield return new WaitForSeconds(1);
        chucky.SetHappy();
        otherChucky.SetHappy();
    }
}