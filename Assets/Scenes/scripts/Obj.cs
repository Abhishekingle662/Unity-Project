// using UnityEngine;

// public class Illuminator : MonoBehaviour
// {
//     public float illuminationRange = 5.0f;
//     private Light pointLight;

//     void Start()
//     {
//         pointLight = GetComponent<Light>();
//         if (pointLight == null)
//         {
//             Debug.LogWarning("No Light component found on " + gameObject.name + ". Illuminator script will not function.");
//             this.enabled = false; // Disable the Illuminator script
//         }
//     }

//     void Update()
// {
//     Collider[] hitColliders = Physics.OverlapSphere(transform.position, illuminationRange);
//     bool objectInRange = false;

//     foreach (var hitCollider in hitColliders)
//     {
//         if (hitCollider.gameObject.tag == "Illuminatable")
// // The tag name must match exactly
//         {
//             objectInRange = true;
//             break;
//         }
//     }

//     pointLight.enabled = objectInRange;
// }

// }





using UnityEngine;

public class DistanceInteraction : MonoBehaviour
{
    public Transform targetObject;
    public float interactionDistance = 5f;

    void Update()
    {
        if (Vector3.Distance(transform.position, targetObject.position) < interactionDistance)
        {
            // Perform interaction (e.g., change color, initiate animation, etc.)
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
