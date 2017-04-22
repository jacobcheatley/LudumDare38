using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
    [SerializeField] private float referencePixelsPerUnit;
    [SerializeField] private int referenceOrthographicSize;
    [SerializeField] private GameObject digger;
    [SerializeField] private GameConstants constants;
    
    private int lastSize = 0;

    public void Start()
    {
        UpdateOrthoSize();
    }

    private void UpdateOrthoSize()
    {
        lastSize = Screen.height;
        float refOrthoSize = (referenceOrthographicSize / referencePixelsPerUnit) * 0.5f;
        float orthoSize = (lastSize / referencePixelsPerUnit) * 0.5f;
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

        Vector3 diggerPosition = digger.transform.position;
        float newY = NearestPixel(diggerPosition.y - 2f);
        transform.position = new Vector3(-0.5f, newY, -10f);
    }

    float NearestPixel(float position)
    {
        return Mathf.RoundToInt(position * referencePixelsPerUnit) / referencePixelsPerUnit;
    }
}
