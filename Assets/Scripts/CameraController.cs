using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Rotation")]
    public int rotationSpeed = 360;
    public int minAngleX = 10;
    public int maxAngleX = 80;

    [Header("Zoom")]
    public int zoomSpeed = 10;
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

        float angleDeltaZ = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.up, angleDeltaZ);

        float angleDeltaX = mouseDelta.y * rotationSpeed * Time.deltaTime;
        float angleX = 90 - Vector3.Angle(transform.forward, -Vector3.up);
        float newAngleX = angleX + angleDeltaX;
        float clampedAngleX = Mathf.Clamp(newAngleX, minAngleX, maxAngleX);
        float clampedAngleDeltaX = clampedAngleX - angleX;
        transform.RotateAround(Vector3.zero, transform.right, clampedAngleDeltaX);
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