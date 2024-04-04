using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public float radius = 5f; // Radius of the circle
    public float speed = 1f; // Speed of the movement
    public Transform targetObject; // Target object to interact with
    public float interactionDistance = 1f; // Distance for interaction
    public Color interactionColor = Color.red; // Color when in interaction range
    private Color originalColor; // Original color to revert to when not interacting
    private Renderer objectRenderer; // Renderer of the object

    private float angle = 0f; // Current angle

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color; // Store the original color
        }
    }

    void Update()
    {
        // Perform circular motion
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, transform.position.y, z);

        // Check distance to the target object
        CheckDistanceToTarget();
    }

    void CheckDistanceToTarget()
    {
        if (targetObject != null && objectRenderer != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.position);
            if (distanceToTarget < interactionDistance)
            {
                objectRenderer.material.color = interactionColor;
            }
            else
            {
                objectRenderer.material.color = originalColor;
            }
        }
    }
}
