using UnityEngine;

public class ShoulderCameraFollow : MonoBehaviour
{
    public Transform target; // Usually the player
    public Vector3 offset = new Vector3(-1.5f, 2f, -3f); // Shoulder position
    public float followSpeed = 5f;
    public float lookAngleOffset = -10f; // Rotate camera slightly left (-) or right (+)

    private void LateUpdate()
    {
        if (target == null) return;

        // Position camera relative to the player’s rotation and position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Create a base rotation looking in the same direction as the player
        Quaternion targetRotation = Quaternion.Euler(0, target.eulerAngles.y + lookAngleOffset, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }
}
