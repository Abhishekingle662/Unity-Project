
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public enum InteractionMode
    {
        None,
        Rotatable,
        Draggable
    }

    public InteractionMode interactionMode;
    
    private Vector3 screenPoint;
    private Vector3 offset;
    private float rotationSpeed = 300.0f;
    private bool isInteracting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isInteracting = true;
                if (interactionMode == InteractionMode.Draggable)
                {
                    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isInteracting = false;
        }

        if (isInteracting && Input.GetMouseButton(0))
        {
            if (interactionMode == InteractionMode.Draggable)
            {
                DragObject();
            }
            else if (interactionMode == InteractionMode.Rotatable)
            {
                RotateObject();
            }
        }
    }

    void DragObject()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void RotateObject()
    {
        float rotationX = Input.GetAxis("Mouse X") *2*  rotationSpeed * Time.deltaTime;
        float rotationY = Input.GetAxis("Mouse Y") *2* rotationSpeed * Time.deltaTime;

        // Apply rotation around y-axis globally and x-axis locally
        transform.Rotate(Vector3.up, -rotationX, Space.World);
        transform.Rotate(Vector3.right, rotationY, Space.World);
    }
}
