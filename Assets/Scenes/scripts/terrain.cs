using UnityEngine;

public class AnimatedPerlinNoiseTerrain : MonoBehaviour
{
    [SerializeField]
    private int width = 15;
    [SerializeField]
    private int depth = 15;
    [SerializeField]
    private float heightScale = 1.0f;
    [SerializeField]
    private float scale = 10.0f;

    private Vector3[] vertices;
    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[(width + 1) * (depth + 1)];

        // Initialize vertices
        for (int i = 0, z = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z); // Initialize with y = 0
            }
        }

        int[] triangles = new int[width * depth * 6];

        // Initialize triangles
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        float timeOffset = Time.time * 2.0f; // Adjust speed of flow here

        for (int i = 0, z = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++, i++)
            {
                float y = Mathf.PerlinNoise((x + timeOffset) * scale / width, z * scale / depth) * heightScale;
                vertices[i].y = y;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}



