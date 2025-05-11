using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DirectionalSelector : MonoBehaviour
{
    public RectTransform topArrow, bottomArrow, leftArrow, rightArrow;
    public Image topArrowImage, bottomArrowImage, leftArrowImage, rightArrowImage;

    public float moveDistance = 20f;
    public float selectionRadius = 50f; // virtual mouse stays in this circle
    public RectTransform virtualCursor;
    public float cursorFollowSpeed = 20f;

    private Vector2 virtualMouse; // simulated position
    private Vector2 center;

    private Image currentHighlightedImage;

    void Start()
    {
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        virtualMouse = Vector2.zero;

        ResetArrowColors(); // Set all arrows to white at start
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        virtualMouse += new Vector2(deltaX, deltaY) * 10f;

        if (virtualMouse.magnitude > selectionRadius)
        {
            virtualMouse = virtualMouse.normalized * selectionRadius;
        }

        UpdateArrows(virtualMouse.normalized);

        if (virtualCursor != null)
        {
            virtualCursor.anchoredPosition = Vector2.Lerp(
                virtualCursor.anchoredPosition,
                virtualMouse,
                Time.deltaTime * cursorFollowSpeed
            );
        }

        // Trigger highlight manually for testing
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HighlightSelectedArrow(1f); // Light attack = 1 sec
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HighlightSelectedArrow(2f); // Heavy attack = 2 sec
        }
    }

    void UpdateArrows(Vector2 dir)
    {
        ResetArrowPositions();

        if (dir.magnitude < 0.2f)
        {
            currentHighlightedImage = null;
            return;
        }

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                rightArrow.anchoredPosition = new Vector2(40 + moveDistance, 0);
                currentHighlightedImage = rightArrowImage;
            }
            else
            {
                leftArrow.anchoredPosition = new Vector2(-40 - moveDistance, 0);
                currentHighlightedImage = leftArrowImage;
            }
        }
        else
        {
            if (dir.y > 0)
            {
                topArrow.anchoredPosition = new Vector2(0, 40 + moveDistance);
                currentHighlightedImage = topArrowImage;
            }
            else
            {
                bottomArrow.anchoredPosition = new Vector2(0, -40 - moveDistance);
                currentHighlightedImage = bottomArrowImage;
            }
        }
    }

    void ResetArrowPositions()
    {
        topArrow.anchoredPosition = new Vector2(0, 40);
        bottomArrow.anchoredPosition = new Vector2(0, -40);
        leftArrow.anchoredPosition = new Vector2(-40, 0);
        rightArrow.anchoredPosition = new Vector2(40, 0);
    }

    void ResetArrowColors()
    {
        topArrowImage.color = Color.white;
        bottomArrowImage.color = Color.white;
        leftArrowImage.color = Color.white;
        rightArrowImage.color = Color.white;
    }

    public void HighlightSelectedArrow(float duration)
    {
        if (currentHighlightedImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(HighlightForDuration(currentHighlightedImage, Color.red, duration));
        }
    }

    IEnumerator HighlightForDuration(Image arrowImage, Color highlightColor, float duration)
    {
        arrowImage.color = highlightColor;
        yield return new WaitForSeconds(duration);
        arrowImage.color = Color.white;
    }
}
