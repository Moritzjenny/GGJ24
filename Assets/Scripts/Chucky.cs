using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chucky : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool leftButtonClick = Mouse.current.leftButton.wasPressedThisFrame;
        bool rightButtonClick = Mouse.current.rightButton.wasPressedThisFrame;

        if (leftButtonClick || rightButtonClick)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                if (leftButtonClick)
                {
                    CameraController.instance.currentPivot = transform;
                }
                else if (rightButtonClick)
                {
                    rigidbody.AddForce(1000 * CameraController.instance.transform.forward);
                }
            }
        }
    }
}