using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform pivot;
    public int animationSpeed = 2;
    public float resetDelay = 1f;

    [Header("Rotation")]
    public int rotationSpeed = 270;
    public int minAngle = 10;
    public int maxAngle = 80;

    [Header("Zoom")]
    public int zoomSpeed = 8;
    public int minDistance = 10;
    public int maxDistance = 100;

    [Header("Chucky")]
    public int chuckyMaxAngle = 170;
    public int chuckyInitialDistance = 10;
    public int chuckyInitialOffsetY = 2;
    public int chuckyMinDistance = 1;
    public int chuckyMaxDistance = 20;

    private Transform initialPivot;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector2 initialMousePosition;

    private bool isAnimating;
    private Vector3 fromPosition;
    private Quaternion fromRotation;
    private Vector3 toPosition;
    private Quaternion toRotation;
    private float t;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        initialPivot = pivot;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (isAnimating)
        {
            Animate();
            return;
        }

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

    private void Animate()
    {
        var newPosition = Vector3.Lerp(fromPosition, toPosition, t);
        var newRotation = Quaternion.Lerp(fromRotation, toRotation, t);
        transform.SetPositionAndRotation(newPosition, newRotation);

        t += animationSpeed * Time.deltaTime;

        if (t >= 1)
        {
            isAnimating = false;
        }
    }

    private void Rotate()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float angleDeltaZ = mouseDelta.x * rotationSpeed * Time.deltaTime;
        transform.RotateAround(pivot.position, Vector3.up, angleDeltaZ);

        float angleDeltaX = mouseDelta.y * rotationSpeed * Time.deltaTime;
        float angleX = Vector3.Angle(-Vector3.up, transform.forward);
        float newAngleX = angleX + angleDeltaX;
        float clampedAngleX = Mathf.Clamp(newAngleX, minAngle, pivot == initialPivot ? maxAngle : chuckyMaxAngle);
        float clampedAngleDeltaX = clampedAngleX - angleX;
        transform.RotateAround(pivot.position, -transform.right, clampedAngleDeltaX);
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
            float distance = Vector3.Distance(transform.position, pivot.position);
            float newDistance = Mathf.Clamp(distance - scrollDelta * zoomSpeed * Time.deltaTime, pivot == initialPivot ? minDistance : chuckyMinDistance, pivot == initialPivot ? maxDistance : chuckyMaxDistance);
            float distanceDelta = newDistance - distance;
            Vector3 direction = (transform.position - pivot.position).normalized;

            transform.position += distanceDelta * direction;
        }
    }

    public void SetPivot(Transform newPivot)
    {
        pivot = newPivot;
        isAnimating = true;
        fromPosition = transform.position;
        fromRotation = transform.rotation;
        var forward = Vector3.ProjectOnPlane(newPivot.forward, Vector3.up).normalized;
        toPosition = newPivot.position - chuckyInitialDistance * forward + chuckyInitialOffsetY * Vector3.up;
        toRotation = Quaternion.LookRotation((newPivot.position - toPosition).normalized, Vector3.up);
        t = 0;
    }

    public void ResetPivot()
    {
        pivot = initialPivot;
        isAnimating = true;
        fromPosition = transform.position;
        fromRotation = transform.rotation;
        toPosition = initialPosition;
        toRotation = initialRotation;
        t = 0;
    }
}