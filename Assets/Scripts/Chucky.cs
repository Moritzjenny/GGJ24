using UnityEngine;
using UnityEngine.InputSystem;

public class Chucky : MonoBehaviour
{
    public static Chucky activeInstance;

    private new Rigidbody rigidbody;
    private ChuckyController chuckyController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
    }
}