using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float destructionTime;
    [SerializeField] private OreRoll[] oreRolls;
    [SerializeField] private float tier;
    [SerializeField] private Color rockColor;

    [HideInInspector] public OreInfo HeldOre;

    private float currentTimeElapsed = 0f;
    private SpriteRenderer renderer;
    private SpriteRenderer oreRenderer;
    private bool inDrill = false;
    private PlayerParts playerParts;
    private SoundPlayer soundPlayer;
    private ParticleSystem.MinMaxGradient particleGradient;

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

        particleGradient = new ParticleSystem.MinMaxGradient(rockColor, HeldOre != null ? HeldOre.ParticleColor : rockColor);
    }

    void Update()
    {
        if (inDrill && playerParts.drillHead.Power >= tier)
        {
            currentTimeElapsed = currentTimeElapsed + Time.deltaTime * playerParts.drillHead.Speed;
            playerParts.UpdateParticleColors(particleGradient);
            playerParts.soundPlayer.SetCrunchLoopVolume(currentTimeElapsed * 3 / destructionTime);
            playerParts.Emission();
        }
        else
        {
            currentTimeElapsed -= Time.deltaTime / 4f;
            currentTimeElapsed = currentTimeElapsed < 0 ? 0 : currentTimeElapsed;
            if (playerParts != null)
                playerParts.soundPlayer.SetCrunchLoopVolume(currentTimeElapsed * 3 / destructionTime);
        }

        float alpha = 1f - (currentTimeElapsed / destructionTime * 0.75f);
        renderer.color = new Color(1f, 1f, 1f, alpha);
        if (oreRenderer != null)
            oreRenderer.color = renderer.color;
        if (currentTimeElapsed >= destructionTime)
        {
            if (HeldOre != null)
            {
                playerParts.AddOre(HeldOre);
                playerParts.soundPlayer.PlayPop();
            }
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
