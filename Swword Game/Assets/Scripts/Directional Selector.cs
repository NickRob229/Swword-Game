using UnityEngine;
using UnityEngine.UI;

public class DirectionalSelector : MonoBehaviour
{
    public RectTransform topArrow, bottomArrow, leftArrow, rightArrow;
    public float moveDistance = 20f;
    public float selectionRadius = 50f; // virtual mouse stays in this circle
    public RectTransform virtualCursor;
    public float cursorFollowSpeed = 20f;

    private Vector2 virtualMouse; // simulated position
    private Vector2 center;

    void Start()
    {
        // Start with center of screen
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        virtualMouse = Vector2.zero;
    }

    void Update()
    {
        // Get movement deltas
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        virtualMouse += new Vector2(deltaX, deltaY) * 10f; // sensitivity multiplier

        // Clamp to a circle around the center
        if (virtualMouse.magnitude > selectionRadius)
        {
            virtualMouse = virtualMouse.normalized * selectionRadius;
        }

        UpdateArrows(virtualMouse.normalized);

        // Move the reticle to match the virtual cursor
        if (virtualCursor != null)
        {
            virtualCursor.anchoredPosition = Vector2.Lerp(
                virtualCursor.anchoredPosition,
                virtualMouse,
                Time.deltaTime * cursorFollowSpeed
            );
        }

    }

    void UpdateArrows(Vector2 dir)
    {
        ResetArrowPositions();

        if (dir.magnitude < 0.2f) return;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                rightArrow.anchoredPosition = new Vector2(40 + moveDistance, 0);
            else
                leftArrow.anchoredPosition = new Vector2(-40 - moveDistance, 0);
        }
        else
        {
            if (dir.y > 0)
                topArrow.anchoredPosition = new Vector2(0, 40 + moveDistance);
            else
                bottomArrow.anchoredPosition = new Vector2(0, -40 - moveDistance);
        }
    }

    void ResetArrowPositions()
    {
        topArrow.anchoredPosition = new Vector2(0, 40);
        bottomArrow.anchoredPosition = new Vector2(0, -40);
        leftArrow.anchoredPosition = new Vector2(-40, 0);
        rightArrow.anchoredPosition = new Vector2(40, 0);
    }
}
