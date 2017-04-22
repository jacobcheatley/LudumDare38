using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    [SerializeField] private float referencePixelsPerUnit;
    [SerializeField] private int referenceOrthographicSize;

    private int lastSize = 0;

    public void Start()
    {
        UpdateOrthoSize();
    }

    private void UpdateOrthoSize()
    {
        lastSize = Screen.height;
        float refOrthoSize = (referenceOrthographicSize / referencePixelsPerUnit) * 0.5f;
        float ppu = referencePixelsPerUnit;
        float orthoSize = (lastSize / ppu) * 0.5f;
        float multiplier = Mathf.Max(1, Mathf.Round(orthoSize / refOrthoSize));
        orthoSize /= multiplier;
        GetComponent<Camera>().orthographicSize = orthoSize;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (lastSize != Screen.height)
            UpdateOrthoSize();
#endif
    }
}
