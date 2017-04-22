using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    [SerializeField] private OreRoll[] oreRolls;
    [SerializeField] private float tier;

    [HideInInspector] public OreInfo HeldOre;
    private float currentTimeElapsed = 0f;
    private SpriteRenderer renderer;
    private bool inDrill = false;
    private PlayerParts playerParts;

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
        if (inDrill && playerParts.drillHead.Power >= tier)
        {
            currentTimeElapsed = currentTimeElapsed + Time.deltaTime * playerParts.drillHead.Speed;
        }
        else
        {
            currentTimeElapsed -= Time.deltaTime;
            currentTimeElapsed = currentTimeElapsed < 0 ? 0 : currentTimeElapsed;
        }
        float alpha = 1f - (currentTimeElapsed / destructionTime * 0.75f);
        renderer.color = new Color(1f, 1f, 1f, alpha);
        if (currentTimeElapsed >= destructionTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Drill")
        {
            inDrill = true;
            playerParts = other.GetComponentInParent(typeof (PlayerParts)) as PlayerParts;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        if (other.tag == "Drill")
            inDrill = false;
    }
}
