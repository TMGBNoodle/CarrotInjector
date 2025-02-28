using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor; // How much this layer moves relative to the camera (0-1)
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }
    
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);
        previousCameraPosition = cameraTransform.position;
    }
}
