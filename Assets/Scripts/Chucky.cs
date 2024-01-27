using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chucky : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Rigidbody mainRigidBody;
    public ChuckyController chuckyController;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        chuckyController = GetComponent<ChuckyController>();
        // Find the attached Rigidbody component
        mainRigidBody = GetComponent<Rigidbody>();
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
                    CameraController.instance.currentPivot = transform;
                }
                else if (rightButtonClick)
                {
                    // Inform ChuckyController about kick, animation and gamelogic affected by the impulse is going to be handled there
                    chuckyController.KickChucky();
                    // Add force to main body to which pelvis is fixely joint schimauscha
                    mainRigidBody.AddForce(10000 * CameraController.instance.transform.forward);

                }
            }
        }
    }
}