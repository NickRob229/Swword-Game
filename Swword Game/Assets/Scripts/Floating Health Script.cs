using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Camera mainCamera;
    public Vector3 worldOffset = new Vector3(0, 2.2f, 0); // Offset above head

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (playerTarget == null || mainCamera == null) return;

        Vector3 worldPos = playerTarget.position + worldOffset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        rectTransform.position = screenPos;
    }
}
