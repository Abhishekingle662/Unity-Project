using UnityEngine;

public class LightDirectionController : MonoBehaviour
{
    public Transform lightSource;   // Assign the light source GameObject
    public float animationSpeed = 1.0f; // Speed of light movement
    public float radius = 2.0f; // Radius of the circle for light movement
    public Color lightColor = Color.white; // Color of the light
    public float lightIntensity = 1.0f; // Intensity of the light

    private Vector3 initialPosition;

    void Start()
    {
        if (lightSource != null)
        {
            initialPosition = lightSource.position;
        }
    }

    void Update()
    {
        if (lightSource != null)
        {
            AnimateLightSource();
            UpdateLightProperties();
        }
    }

    void AnimateLightSource()
    {
        float x = Mathf.Cos(Time.time * animationSpeed) * radius;
        float z = Mathf.Sin(Time.time * animationSpeed) * radius;

        lightSource.position = new Vector3(x, initialPosition.y, z) + initialPosition;
    }

    void UpdateLightProperties()
{
    GameObject[] affectedObjects = GameObject.FindGameObjectsWithTag("LightAffected");
    foreach (GameObject obj in affectedObjects)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            Material objMaterial = objRenderer.material;
            if (objMaterial != null)
            {
                objMaterial.SetVector("_LightPosition", lightSource.position);
                objMaterial.SetColor("_LightColor", lightColor * lightIntensity);
                objMaterial.SetFloat("_LightIntensity", lightIntensity);
            }
        }
    }
}

}
