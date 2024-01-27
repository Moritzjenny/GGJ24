using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform pivot;
    public Transform currentPivot;

    [Header("Rotation")]
    public int rotationSpeed = 360;
    public int minAngle = 10;
    public int maxAngle = 80;
    public int maxAngleChucky = 170;

    [Header("Zoom")]
    public int zoomSpeed = 10;
    public int minDistance = 10;
    public int maxDistance = 100;

    private Vector2 initialMousePosition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentPivot = pivot;
    }

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
        transform.RotateAround(currentPivot.position, Vector3.up, angleDeltaZ);

        float angleDeltaX = -mouseDelta.y * rotationSpeed * Time.deltaTime;
        float angleX = Vector3.Angle(-Vector3.up, transform.forward);
        float newAngleX = angleX + angleDeltaX;
        float clampedAngleX = Mathf.Clamp(newAngleX, minAngle, currentPivot == pivot ? maxAngle : maxAngleChucky);
        float clampedAngleDeltaX = clampedAngleX - angleX;
        transform.RotateAround(currentPivot.position, -transform.right, clampedAngleDeltaX);
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
            Vector3 direction = (transform.position - currentPivot.position).normalized;
            float distance = Vector3.Distance(transform.position, currentPivot.position);
            float newDistance = Mathf.Clamp(distance - scrollDelta * zoomSpeed * Time.deltaTime, minDistance, maxDistance);

            transform.position = newDistance * direction;
        }
    }
}