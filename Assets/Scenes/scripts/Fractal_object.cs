using UnityEngine;

public class FractalTreeGenerator : MonoBehaviour
{
    public GameObject branchPrefab;
    public Material fractalMaterial;
    public Color color1;
    public Color color2;
    public int maxDepth = 5;
    public float branchLength = 1.0f;
    public float branchAngle = 30.0f;
    public float angleVariation = 25.0f;
    public float lengthVariation = 0.2f;
    public float distanceBetweenBranches = 1.0f; // Increase this value to space out branches

void Update()
    {
        if (fractalMaterial != null)
        {
            fractalMaterial.SetColor("_Color1", color1);
            fractalMaterial.SetColor("_Color2", color2);
            // ... Set more colors as needed
        }
    }
    private void Start() {
        if (fractalMaterial != null) {
            fractalMaterial.SetColor("_Color1", color1);
            fractalMaterial.SetColor("_Color2", color2);
        }
        GenerateTree(transform, maxDepth, branchLength, Quaternion.identity);
    }

    private void GenerateTree(Transform parent, int depth, float length, Quaternion rotation)
{
    if (depth <= 0)
        return;

    GameObject branch = Instantiate(branchPrefab);
    branch.transform.SetParent(parent);

    // Call the function to assign a random color to the branch
    // AssignRandomColor(branch);

    branch.transform.localPosition = Vector3.up * length;
    branch.transform.localRotation = rotation;

    // Randomize branch properties
    float randomAngle = Random.Range(-branchAngle, branchAngle);
    Quaternion childRotation = Quaternion.Euler(Vector3.up * randomAngle);

    float randomLength = length + lengthVariation * Random.Range(-1f, 1f);

    // Increase the distance between branches
    Vector3 childPosition = Vector3.up * (length + distanceBetweenBranches);

    // Apply a random horizontal rotation
    float horizontalRotation = Random.Range(-angleVariation, angleVariation);
    childPosition = Quaternion.Euler(Vector3.forward * horizontalRotation) * childPosition;

    branch.transform.localPosition = childPosition;
    branch.transform.localRotation = rotation * childRotation;

    // Recursively generate child branches
    GenerateTree(branch.transform, depth - 1, randomLength * 1.5f, rotation * childRotation);
    GenerateTree(branch.transform, depth - 1, randomLength * 1.0f, rotation * Quaternion.Inverse(childRotation));
}


    private void AssignRandomColor(GameObject branch)
{
    Renderer renderer = branch.GetComponent<Renderer>();
    if (renderer != null)
    {
        Material newMat = new Material(Shader.Find("Custom/SimplePointLightWithAmbientSpecular"));
        
        // Adjust this line based on your shader's color property
        newMat.SetColor("YourShaderColorProperty", Random.ColorHSV());

        renderer.material = newMat;
    }
}


}


























// using UnityEngine;

// public class FractalTreeGenerator : MonoBehaviour {
//     public GameObject branchPrefab;
//     public int maxDepth = 5;
//     public float branchLength = 1.0f;
//     public float branchAngle = 30.0f;
//     public float angleVariation = 25.0f;
//     public float lengthVariation = 0.2f;
//     public float distanceBetweenBranches = 1.0f;

//     private void Start() {
//         GenerateTree(transform, maxDepth, branchLength, Quaternion.identity);
//     }

//     private void GenerateTree(Transform parent, int depth, float length, Quaternion rotation) {
//         if (depth <= 0)
//             return;

//         GameObject branch = Instantiate(branchPrefab);
//         branch.transform.SetParent(parent);
//         branch.transform.localPosition = Vector3.up * length;
//         branch.transform.localRotation = rotation;

//         float randomAngle = Random.Range(-branchAngle, branchAngle);
//         Quaternion childRotation = Quaternion.Euler(Vector3.up * randomAngle);

//         float randomLength = length + lengthVariation * Random.Range(-1f, 1f);
//         Vector3 childPosition = Vector3.up * (length + distanceBetweenBranches);
//         float horizontalRotation = Random.Range(-angleVariation, angleVariation);
//         childPosition = Quaternion.Euler(Vector3.forward * horizontalRotation) * childPosition;

//         branch.transform.localPosition = childPosition;
//         branch.transform.localRotation = rotation * childRotation;

//         GenerateTree(branch.transform, depth - 1, randomLength * 1.5f, rotation * childRotation);
//         GenerateTree(branch.transform, depth - 1, randomLength * 1.0f, rotation * Quaternion.Inverse(childRotation));
//     }
// }


















