using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour
{
    [SerializeField] private Text text;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void Init(string text)
    {
        this.text.text = text;
    }
}
