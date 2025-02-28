using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign your player's transform here
    public float smoothSpeed = 0.125f; // Lower for smoother camera, higher for more responsive
    public Vector3 offset = new Vector3(0, 1, -10); // Adjust to position camera relative to player

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;
        
        // Smoothly move camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}