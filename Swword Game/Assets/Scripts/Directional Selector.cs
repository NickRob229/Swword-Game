using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DirectionalSelector : MonoBehaviour
{
    public RectTransform topArrow, bottomArrow, leftArrow, rightArrow;
    public float moveDistance = 20f;
    public float selectionRadius = 50f;
    public RectTransform virtualCursor;
    public float cursorFollowSpeed = 20f;

    [Header("Visual Settings")]
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    [Header("Attack Cooldowns")]
    public float lightAttackCooldown = 1f;
    public float heavyAttackCooldown = 2f;

    private Vector2 virtualMouse;
    private Vector2 center;
    private string currentDirection = "";
    private bool isOnCooldown = false;

    void Start()
    {
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        virtualMouse = Vector2.zero;
        ResetArrowColors();
    }

    void Update()
    {
        if (!isOnCooldown)
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            virtualMouse += new Vector2(deltaX, deltaY) * 10f;

            if (virtualMouse.magnitude > selectionRadius)
            {
                virtualMouse = virtualMouse.normalized * selectionRadius;
            }

            UpdateArrows(virtualMouse.normalized);
        }

        if (virtualCursor != null)
        {
            virtualCursor.anchoredPosition = Vector2.Lerp(
                virtualCursor.anchoredPosition,
                virtualMouse,
                Time.deltaTime * cursorFollowSpeed
            );
        }

        if (!isOnCooldown)
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                StartCoroutine(PerformAttack("Light", lightAttackCooldown));
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                StartCoroutine(PerformAttack("Heavy", heavyAttackCooldown));
            }
        }
    }

    void UpdateArrows(Vector2 dir)
    {
        ResetArrowPositions();

        if (dir.magnitude < 0.2f)
        {
            currentDirection = "";
            ResetArrowColors(); // No input, reset visuals
            return;
        }

        ResetArrowColors(); // Only reset color when not on cooldown

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                rightArrow.anchoredPosition = new Vector2(40 + moveDistance, 0);
                currentDirection = "Right";
            }
            else
            {
                leftArrow.anchoredPosition = new Vector2(-40 - moveDistance, 0);
                currentDirection = "Left";
            }
        }
        else
        {
            if (dir.y > 0)
            {
                topArrow.anchoredPosition = new Vector2(0, 40 + moveDistance);
                currentDirection = "Top";
            }
            else
            {
                bottomArrow.anchoredPosition = new Vector2(0, -40 - moveDistance);
                currentDirection = "Bottom";
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
        topArrow.GetComponent<Image>().color = defaultColor;
        bottomArrow.GetComponent<Image>().color = defaultColor;
        leftArrow.GetComponent<Image>().color = defaultColor;
        rightArrow.GetComponent<Image>().color = defaultColor;
    }

    IEnumerator PerformAttack(string attackType, float cooldown)
    {
        if (string.IsNullOrEmpty(currentDirection)) yield break;

        isOnCooldown = true;

        string fullAttackName = currentDirection + " " + attackType + " Attack";
        Debug.Log("Performing: " + fullAttackName);

        HighlightSelectedArrow();

        yield return new WaitForSeconds(cooldown);

        ResetArrowColors();
        isOnCooldown = false;
    }

    void HighlightSelectedArrow()
    {
        if (currentDirection == "Top")
            topArrow.GetComponent<Image>().color = highlightColor;
        else if (currentDirection == "Bottom")
            bottomArrow.GetComponent<Image>().color = highlightColor;
        else if (currentDirection == "Left")
            leftArrow.GetComponent<Image>().color = highlightColor;
        else if (currentDirection == "Right")
            rightArrow.GetComponent<Image>().color = highlightColor;
    }
}
