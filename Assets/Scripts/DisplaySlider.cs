using UnityEngine;

public class DisplaySlider : MonoBehaviour
{
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetSize(float size)
    {
        rect.localScale = new Vector3(size, 1, 1);
    }
}
