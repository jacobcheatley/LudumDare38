using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    [Header("Drill Parts")]
    [SerializeField] private GameObject drillAxis;
    [SerializeField] private GameObject drillEffector;
    [SerializeField] private GameObject drillHead;

    [Header("UI")]
    [SerializeField] private FuelDisplay fuelDisplay;
    [SerializeField] private Text fuelText;
    [SerializeField] public GameObject shopCanvas;
    [SerializeField] private GameObject informationCanvas;
    [SerializeField] private Text depthText;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject errorPrefab;
    [SerializeField] public GameObject hudCanvas;

    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource drillAudioSource;
    [SerializeField] private AudioSource idleAudioSource;
    [SerializeField] private AudioSource revAudioSource;
    [SerializeField] private AudioClip higherDrillClip;
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private AudioMixerSnapshot defaultSnapshot;
    [SerializeField] private AudioMixerSnapshot mutedSnapshot;
    [SerializeField] private AudioMixerSnapshot winSnapshot;
    [SerializeField] private AudioMixerSnapshot loseSnapshot;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator drillAnimator;
    private PlayerParts parts;

    private bool drilling;
    private bool thrusting;
    private float fuel;
    private float health;
    private bool inShop;
    private ParticleSystem.EmissionModule em;
    private float rate = 0f;

    // Audio
    private float drillVolume = 0f;
    private float revVolume = 0f;
    private bool setHigherDrill = false;
    private bool end = false;

    void Start()
    {
        mixer.TransitionToSnapshots(new[] { defaultSnapshot }, new[] { 1f }, 0f);
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        drillAnimator = drillHead.GetComponent<Animator>();
        parts = GetComponent<PlayerParts>();
        em = particles.emission;
        fuel = parts.fuelTank.MaxFuel;
    }

    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("HorizontalMove");
        rb.AddForce(Vector2.right * horizontal);

        float vertical = Input.GetAxisRaw("VerticalMove");
        thrusting = vertical > 0;
        if (thrusting)
        {
            rb.AddForce(Vector2.up * 2.5f * parts.thrusters.Power);
            rate = 15f * parts.thrusters.Power;
        }
        else
            rate -= Time.deltaTime * 30f;

        em.rateOverTime = Mathf.Clamp(rate, 0f, 20f);


        // Drill Rotation
        if (!inShop)
        {
            float verticalDrill = Input.GetAxisRaw("VerticalDrill");
            float horizontalDrill = Input.GetAxisRaw("HorizontalDrill");
            float currentAngle = drillAxis.transform.rotation.eulerAngles.z;
            float newAngle = currentAngle;

            if (verticalDrill != 0 || horizontalDrill != 0)
            {
                newAngle = Mathf.Atan2(verticalDrill, horizontalDrill) * Mathf.Rad2Deg;
                drilling = true;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                newAngle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
                drilling = true;
            }
            else
            {
                drilling = false;
            }

            drillAxis.transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, newAngle, Time.deltaTime * 20f), Vector3.forward);
        }

        // Audio logic
        if (!setHigherDrill && parts.drillHead.Power >= 2)
        {
            drillAudioSource.clip = higherDrillClip;
            drillAudioSource.Play();
            setHigherDrill = true;
        }
        drillVolume += drilling ? Time.deltaTime : -Time.deltaTime;
        drillVolume = Mathf.Clamp01(drillVolume);
        drillAudioSource.volume = drillVolume / 3f;
        
        if (thrusting || Mathf.Abs(horizontal) > 0)
            revVolume += Time.deltaTime / 2;
        else
            revVolume -= Time.deltaTime / 2;
        revVolume = Mathf.Clamp01(revVolume);
        revAudioSource.volume = revVolume / 8f;

        // Ambience
        if (Random.value < 0.1f * Time.deltaTime && transform.position.y >= -4)
            soundPlayer.PlayAboveGround(Mathf.Clamp01((transform.position.y + 4) / 32f));

        if (Random.value < 0.05f * Time.deltaTime && transform.position.y <= -8)
        {
            float sqrtVol = Mathf.Clamp01(Mathf.Abs(transform.position.y - 8) / 100f);
            soundPlayer.PlayBelowGround(sqrtVol * sqrtVol);
            StartCoroutine(ShakeIt(sqrtVol));
        }

        // BG Music
        if (!end)
            soundPlayer.SetMainLoopVolume(Mathf.Clamp01(Mathf.Abs(transform.position.y - 32) / 700f));
        else
            soundPlayer.SetMainLoopVolume(Mathf.Lerp(soundPlayer.GetMainLoopVolume(), 0.25f, Time.deltaTime / 3f));

        // Additional drilling and resource logic
        drillEffector.SetActive(drilling);
        drillAnimator.SetFloat("Speed", drillVolume * drillVolume * 1.5f);

        // Fuel
        fuel -= drilling ? Time.deltaTime * 4f : 0;
        fuel -= thrusting ? Time.deltaTime * 4f : 0;

        fuelDisplay.SetPercent(fuel / parts.fuelTank.MaxFuel);
        fuelText.text = ((int)fuel).ToString();

        if (fuel <= 0)
            Lose();

        depthText.text = string.Format("Depth:{0,3:##0}", -Mathf.RoundToInt(transform.position.y - 1));
    }

    IEnumerator ShakeIt(float intensity)
    {
        yield return new WaitForSeconds(0.15f);
        int shakes = 6;
        while (shakes > 0)
        {
            shakes--;
            rb.AddForce(Random.insideUnitCircle.normalized * intensity * 10f);
            yield return new WaitForSeconds(0.15f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shop")
        {
            inShop = true;
            shopCanvas.SetActive(true);
        }

        if (other.tag == "Information")
        {
            inShop = true;
            informationCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Shop")
        {
            inShop = false;
            shopCanvas.SetActive(false);
        }

        if (other.tag == "Information")
        {
            inShop = false;
            informationCanvas.SetActive(false);
        }
    }

    public int RefuelCost()
    {
        return (int)((parts.fuelTank.MaxFuel - fuel) / 2);
    }

    public void Refuel()
    {
        float quantity = parts.fuelTank.MaxFuel - fuel;
        BuyFuel(quantity);
    }

    public void BuyFuel(float quantity)
    {
        int price = (int)(quantity / 2);
        if (parts.Money >= price)
        {
            parts.Money -= price;
            fuel += quantity;
            soundPlayer.PlayFuel();
        }
        else
        {
            GameObject error = Instantiate(errorPrefab, Vector3.one * 256, Quaternion.identity, shopCanvas.transform);
            error.GetComponent<ErrorText>().Init("Can't afford that fuel.");
            soundPlayer.PlayFail();
        }
    }

    public void RefreshFuel()
    {
        fuel = parts.fuelTank.MaxFuel;
        soundPlayer.PlayFuel();
    }

    public void Win()
    {
        end = true;
        mixer.TransitionToSnapshots(new[] { winSnapshot }, new[] { 1f }, 3f);
        winCanvas.SetActive(true);
    }

    public void Lose()
    {
        end = true;
        mixer.TransitionToSnapshots(new [] { loseSnapshot }, new [] { 1f }, 3f);
        gameOverCanvas.SetActive(true);
    }
}
