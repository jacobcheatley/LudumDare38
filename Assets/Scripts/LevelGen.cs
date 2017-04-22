using UnityEngine;

[System.Serializable]
public class Layer
{
    public int Height;
    public GameObject Block;
}

public class LevelGen : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private Layer[] layers;

    void Start()
    {
        int y = 0;
        foreach (Layer layer in layers)
        {
            for (int yy = 0; yy < layer.Height; yy++)
            {
                for (int xx = 0; xx < width; xx++)
                    Instantiate(layer.Block, Vector3.down * y + Vector3.right * xx, Quaternion.identity);
                y++;
            }
        }
    }
}
