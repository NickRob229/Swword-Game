using UnityEngine;

public class ArrowFollowUI : MonoBehaviour
{
    public Transform playerTarget;
    public Camera mainCamera;
    public RectTransform uiRoot;

    public Vector3 worldOffset = new Vector3(0, 2f, 0); // Vertical offset in world space
    public bool scaleWithDistance = true;
    public float baseDistance = 5f;
    public float scaleMultiplier = 1f;

    void Update()
    {
        if (playerTarget == null || mainCamera == null || uiRoot == null) return;

        // Add offset in world space BEFORE converting to screen position
        Vector3 worldPos = playerTarget.position + worldOffset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        uiRoot.position = screenPos;

        if (scaleWithDistance)
        {
            float distance = Vector3.Distance(mainCamera.transform.position, playerTarget.position);
            float scale = (baseDistance / distance) * scaleMultiplier;
            scale = Mathf.Clamp(scale, 0.5f, 2f); // Prevent it from becoming too large or small
            uiRoot.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
