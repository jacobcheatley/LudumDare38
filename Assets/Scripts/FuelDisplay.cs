using UnityEngine;
using UnityEngine.UI;

public class FuelDisplay : MonoBehaviour
{
    private RectTransform rect;
    private Image image;
    [SerializeField] private Gradient gradient;
    [SerializeField] private GameObject warning;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void SetPercent(float percent)
    {
        rect.localScale = new Vector3(percent, 1, 1);
        image.color = gradient.Evaluate(percent);

        if (percent < 0.2f)
            warning.SetActive(true);
        if (percent > 0.25f)
            warning.SetActive(false);
    }
}
