using System;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Layer
{
    public int Height;
    public GameObject Block;
    public bool HasCaves;
    public Color BackgroundColor;
}

public class LevelGen : MonoBehaviour
{
    [SerializeField] private Layer[] layers;
    [Header("Other")]
    [SerializeField] private GameConstants constants;
    [SerializeField] private GameObject boundary;
    [SerializeField] private float caveThreshold;
    [SerializeField] private GameObject background;

    void Start()
    {
        Vector2 perlinOffset = Random.insideUnitCircle * 16f;
        float halfWidth = constants.Width / 2f;

        // Place blocks
        int y = 0;
        foreach (Layer layer in layers)
        {
            GameObject bg = Instantiate(background, new Vector3(-0.5f, -y - layer.Height / 2, 20), Quaternion.identity, gameObject.transform);
            bg.GetComponent<SpriteRenderer>().color = layer.BackgroundColor;
            bg.transform.localScale = new Vector3(16, layer.Height, 1);
            for (int yy = 0; yy < layer.Height; yy++)
            {
                for (int xx = 0; xx < constants.Width; xx++)
                {
                    float caveValue = 1f;
                    if (layer.HasCaves)
                        caveValue = Mathf.PerlinNoise(xx / 8f + perlinOffset.x, y / 4f + perlinOffset.y);

                    if (caveValue > caveThreshold)
                        Instantiate(layer.Block, Vector3.down * y + Vector3.right * (xx - halfWidth), Quaternion.identity, this.gameObject.transform);
                }
                y++;
            }
        }

        // Place boundary
        int i = -constants.AboveHeight;
        // Top row
        for (int xx = -1; xx < constants.Width + 1; xx++)
            Instantiate(boundary, Vector3.up * constants.AboveHeight + Vector3.right * (xx - halfWidth), Quaternion.identity, this.gameObject.transform);
        // Sides
        i++;
        for (; i < y; i++)
        {
            Instantiate(boundary, Vector3.left * (halfWidth + 1) + Vector3.down * i, Quaternion.identity, this.gameObject.transform);
            Instantiate(boundary, Vector3.right * halfWidth + Vector3.down * i, Quaternion.identity, this.gameObject.transform);
        }
        // Bottom row
        for (int xx = -1; xx < constants.Width + 1; xx++)
            Instantiate(boundary, Vector3.down * y + Vector3.right * (xx - halfWidth), Quaternion.identity, this.gameObject.transform);
    }
}
