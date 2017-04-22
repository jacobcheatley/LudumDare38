using UnityEngine;

[System.Serializable]
public class Layer
{
    public int Height;
    public GameObject Block;
}

public class LevelGen : MonoBehaviour
{
    [SerializeField] private Layer[] layers;
    [SerializeField] private GameConstants constants;

    void Start()
    {
        int y = 0;
        foreach (Layer layer in layers)
        {
            for (int yy = 0; yy < layer.Height; yy++)
            {
                for (int xx = 0; xx < constants.Width; xx++)
                    Instantiate(layer.Block, Vector3.down * y + Vector3.right * (xx - constants.Width / 2f), Quaternion.identity, this.gameObject.transform);
                y++;
            }
        }
    }
}
