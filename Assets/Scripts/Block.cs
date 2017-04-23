using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    [SerializeField] private OreRoll[] oreRolls;
    [SerializeField] private float tier;

    [HideInInspector] public OreInfo HeldOre;

    private float currentTimeElapsed = 0f;
    private SpriteRenderer renderer;
    private SpriteRenderer oreRenderer;
    private bool inDrill = false;
    private PlayerParts playerParts;
    private SoundPlayer soundPlayer;

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
                GameObject ore = Instantiate(HeldOre.OrePrefab, gameObject.transform.position + Vector3.back, Quaternion.identity, this.gameObject.transform);
                oreRenderer = ore.GetComponent<SpriteRenderer>();
                break;
            }
        }
    }

    void Update()
    {
        if (inDrill && playerParts.drillHead.Power >= tier)
        {
            currentTimeElapsed = currentTimeElapsed + Time.deltaTime * playerParts.drillHead.Speed;
            playerParts.soundPlayer.SetCrunchLoopVolume(currentTimeElapsed * 3 / destructionTime);
        }
        else
        {
            currentTimeElapsed -= Time.deltaTime / 4f;
            currentTimeElapsed = currentTimeElapsed < 0 ? 0 : currentTimeElapsed;
        }
        float alpha = 1f - (currentTimeElapsed / destructionTime * 0.75f);
        renderer.color = new Color(1f, 1f, 1f, alpha);
        if (oreRenderer != null)
            oreRenderer.color = renderer.color;
        if (currentTimeElapsed >= destructionTime)
        {
            if (HeldOre != null)
                playerParts.AddOre(HeldOre);
            Destroy(gameObject);
            playerParts.soundPlayer.PlayCrack();
            playerParts.soundPlayer.SetCrunchLoopVolume(0);
        }
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
        if (other.tag == "Drill")
            inDrill = false;
    }
}
