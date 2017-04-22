using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    [SerializeField] private OreRoll[] oreRolls;

    [HideInInspector] public OreInfo HeldOre;
    private float currentTimeElapsed = 0f;
    private SpriteRenderer renderer;
    private bool inDrill = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        // Spawn ore
        float roll = Random.value;
        foreach (OreRoll oreRoll in oreRolls)
        {
            if (roll <= oreRoll.Value)
            {
                HeldOre = oreRoll.Info;
                Instantiate(HeldOre.OrePrefab, this.gameObject.transform);
                break;
            }
        }
    }

    void Update()
    {
        currentTimeElapsed = inDrill ? currentTimeElapsed + Time.deltaTime : Mathf.Clamp(currentTimeElapsed - Time.deltaTime, 0f, destructionTime);
        float alpha = 1f - (currentTimeElapsed / destructionTime * 0.75f);
        renderer.color = new Color(1f, 1f, 1f, alpha);
        if (currentTimeElapsed >= destructionTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Drill")
            inDrill = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Drill")
            inDrill = false;
    }
}
