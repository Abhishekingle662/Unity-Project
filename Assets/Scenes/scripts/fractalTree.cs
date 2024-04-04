// using UnityEngine;

// public class  : MonoBehaviour {
//     public int recurse = 5;
//     public int splitNumber = 2;
//     public Vector3 pivotPosition;

//     void Start() {
//         recurse -= 1;
//         for (int i = 0; i < splitNumber; ++i) {
//             if (recurse > 0) {
//                 var copy = Instantiate(gameObject);
//                 var recursive = copy.GetComponent<FractalTreeGenerator>();
//                 recursive.Generated(new RecursiveBundle() { Index = i, Parent = this });
//             }
//         }
//     }

//     public void Generated(RecursiveBundle bundle) {
//         var parentBody = bundle.Parent.GetComponent<Rigidbody>();
//         var rigidbody = GetComponent<Rigidbody>();
//         var hinge = GetComponent<HingeJoint>();

//         rigidbody.isKinematic = false;
//         hinge.connectedBody = parentBody;
//         hinge.autoConfigureConnectedAnchor = false;
//         hinge.connectedAnchor = bundle.Parent.pivotPosition;

//         this.transform.position += this.transform.up * this.transform.localScale.y;
//     }
// }

// public class RecursiveBundle {
//     public int Index { get; set; }
//     public RecursiveInstantiator Parent { get; set; }
// }

// public class RecursiveInstantiator : MonoBehaviour {}
