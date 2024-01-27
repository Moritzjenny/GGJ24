using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chucky : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    public ChuckyController chuckyController;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        chuckyController = GetComponent<ChuckyController>();
    }

    private void Update()
    {
        bool leftButtonClick = Mouse.current.leftButton.wasPressedThisFrame;
        bool rightButtonClick = Mouse.current.rightButton.wasPressedThisFrame;

        if (leftButtonClick || rightButtonClick)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.transform.IsChildOf(transform))
            {
                print("click");
                if (leftButtonClick)
                {
                    CameraController.instance.SetPivot(transform);
                }
                else if (rightButtonClick)
                {
                    CameraController.instance.ResetPivot();

                    // Inform ChuckyController about kick, animation and gamelogic affected by the impulse is going to be handled there
                    chuckyController.KickChucky();
                    foreach (var rigidbody in rigidbodies)
                    {
                        rigidbody.AddForce(1000 * CameraController.instance.transform.forward);
                    }
                }
            }
        }
    }
}