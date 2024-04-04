using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public List<Transform> targets;
    public int currentTargetIndex = 0;
    public float orbitDistance = 5.0f;
    public float orbitSpeed = 5.0f;
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 1.0f;
    public float smoothing = 1.0f; // Smoothing factor

    private bool isOrbiting = false; // Start with static camera
    private bool isMouseLookEnabled = false;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;
    private Vector3 previousMousePosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        previousMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) // Press 'O' to start orbiting
        {
            isOrbiting = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isOrbiting = !isOrbiting;
            ToggleCursor();
        }

        if (Input.GetKeyDown(KeyCode.Y) && targets.Count > 0)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
        }

        if (isOrbiting)
        {
            Transform currentTarget = targets[currentTargetIndex];
            if (currentTarget != null)
            {
                OrbitTarget(currentTarget);
            }
        }
        else
        {
            HandleMovement();
            if (isMouseLookEnabled)
            {
                HandleMouseLook();
            }
        }
    }

    private void ToggleCursor()
    {
        if (isMouseLookEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OrbitTarget(Transform target)
    {
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        transform.LookAt(target);
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * orbitDistance;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0f;
        if (Input.GetKey(KeyCode.E)) y = 1f;
        else if (Input.GetKey(KeyCode.Q)) y = -1f;
        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
        transform.position += move * movementSpeed * Time.deltaTime;
    }

    private void HandleMouseLook()
    {
        Vector3 mouseDelta = Input.mousePosition - previousMousePosition;
        previousMousePosition = Input.mousePosition;
        float lookX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float lookY = -mouseDelta.y * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, lookX, Space.World);
        transform.Rotate(Vector3.right, lookY, Space.Self);
    }

    public void ToggleMouseLook()
    {
        isMouseLookEnabled = !isMouseLookEnabled;
        ToggleCursor();
    }
}
