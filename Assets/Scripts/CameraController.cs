using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Rotation")]
    public int rotationSpeed = 360;
    public int minAngle = 10;
    public int maxAngle = 90;

    [Header("Zoom")]
    public float zoomSpeed = 1f;
    public int minZoomDistance = 10;
    public int maxZoomDistance = 100;

    private Vector2 initialMousePosition;

    private void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Rotate();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            HideCursor();
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            ShowCursor();
        }

        Zoom();
    }

    private void Rotate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float rotationXAngle = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.up, rotationXAngle);

        float rotationYAngle = mouseDelta.y * rotationSpeed * Time.deltaTime;
        float newVerticalAngle = Mathf.Clamp(transform.eulerAngles.x - rotationYAngle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(newVerticalAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void HideCursor()
    {
        initialMousePosition = Mouse.current.position.ReadValue();
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Mouse.current.WarpCursorPosition(initialMousePosition);
        Cursor.visible = true;
    }

    private void Zoom()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y;
        if (scrollDelta != 0)
        {
            Vector3 direction = (transform.position - Vector3.zero).normalized;
            float distance = Vector3.Distance(transform.position, Vector3.zero);
            float newDistance = Mathf.Clamp(distance - scrollDelta * zoomSpeed * Time.deltaTime, minZoomDistance, maxZoomDistance);

            transform.position = newDistance * direction;
        }
    }
}