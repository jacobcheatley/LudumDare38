using UnityEngine;
using UnityEngine.UI;

public class SimpleButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    private static readonly Color halfWhite = new Color(1, 1, 1, 0.5f);

    void Start()
    {
        image.color = halfWhite;
    }

    public void Hovering(bool hover)
    {
        image.color = hover ? Color.white : halfWhite;
    }
}
