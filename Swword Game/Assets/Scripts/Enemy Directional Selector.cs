using UnityEngine;
using UnityEngine.UI;

public class EnemyDirectionalSelector : MonoBehaviour
{
    public RectTransform topArrow, bottomArrow, leftArrow, rightArrow;
    public float moveDistance = 20f;

    [Header("Visual Settings")]
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.gray;

    void Start()
    {
        ResetArrowPositions();
        ResetArrowColors();
    }

    public void ForceDirection(Vector2 dir)
    {
        UpdateArrows(dir.normalized);
    }

    void UpdateArrows(Vector2 dir)
    {
        ResetArrowPositions();
        ResetArrowColors();

        if (dir.magnitude < 0.2f)
        {
            return;
        }

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                rightArrow.anchoredPosition = new Vector2(40 + moveDistance, 0);
                rightArrow.GetComponent<Image>().color = highlightColor;
            }
            else
            {
                leftArrow.anchoredPosition = new Vector2(-40 - moveDistance, 0);
                leftArrow.GetComponent<Image>().color = highlightColor;
            }
        }
        else
        {
            if (dir.y > 0)
            {
                topArrow.anchoredPosition = new Vector2(0, 40 + moveDistance);
                topArrow.GetComponent<Image>().color = highlightColor;
            }
            else
            {
                bottomArrow.anchoredPosition = new Vector2(0, -40 - moveDistance);
                bottomArrow.GetComponent<Image>().color = highlightColor;
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

    public void ResetArrowColors()
    {
        topArrow.GetComponent<Image>().color = defaultColor;
        bottomArrow.GetComponent<Image>().color = defaultColor;
        leftArrow.GetComponent<Image>().color = defaultColor;
        rightArrow.GetComponent<Image>().color = defaultColor;
    }
}
