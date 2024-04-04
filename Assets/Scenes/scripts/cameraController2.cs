// using UnityEngine;

// public class OrbitingCameraController : MonoBehaviour
// {
//     public Transform target;
//     public float orbitDistance = 10.0f;
//     public float orbitSpeed = 5.0f;

//     private WalkingCameraController walkingCameraController;

//     private bool isOrbiting = true; // Track the current state

//     private void Start()
//     {
//         walkingCameraController = GetComponent<WalkingCameraController>();
//         if (walkingCameraController != null)
//         {
//             walkingCameraController.enabled = !isOrbiting; // Enable/disable based on the initial state
//         }
//     }

//     private void Update()
//     {
//         // Toggle between Walking and Orbiting Camera Controllers
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             isOrbiting = !isOrbiting; // Toggle the state
//             if (isOrbiting)
//             {
//                 // Enable Orbiting Camera Controller and disable Walking Camera Controller
//                 enabled = true;
//                 if (walkingCameraController != null)
//                 {
//                     walkingCameraController.enabled = false;
//                 }
//             }
//             else
//             {
//                 // Enable Walking Camera Controller and disable Orbiting Camera Controller
//                 enabled = false;
//                 if (walkingCameraController != null)
//                 {
//                     walkingCameraController.enabled = true;
//                 }
//             }
//         }

//         if (target != null && isOrbiting)
//         {
//             OrbitTarget();
//         }
//     }

//     private void OrbitTarget()
//     {
//         transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
//         transform.LookAt(target);

//         Vector3 direction = (transform.position - target.position).normalized;
//         transform.position = target.position + direction * orbitDistance;
//     }
// }
