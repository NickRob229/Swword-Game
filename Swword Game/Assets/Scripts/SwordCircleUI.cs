using UnityEngine;
using UnityEngine.UI;

public class SwordCircleUI : MonoBehaviour
{
    public RawImage[] swords;
    public float radius = 200f;

    void Start()
    {
        PositionSwords();
    }

    void PositionSwords()
    {
        int count = swords.Length;
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
            swords[i].rectTransform.anchoredPosition = pos;

            // Make the sword face inward (toward center)
            swords[i].rectTransform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        }
    }
}