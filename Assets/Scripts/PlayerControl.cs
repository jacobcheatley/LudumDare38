using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Drill Parts")]
    [SerializeField] private GameObject drillAxis;
    [SerializeField] private GameObject drillEffector;
    [SerializeField] private GameObject drillHead;

    [Header("UI")]
    [SerializeField] private FuelDisplay fuelDisplay;
    [SerializeField] private Text fuelText;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject informationCanvas;

    [Header("Audio")]
    [SerializeField] private AudioSource drillAudioSource;
    [SerializeField] private AudioSource idleAudioSource;
    [SerializeField] private AudioSource revAudioSource;
    [SerializeField] private AudioClip higherDrillClip;
    [SerializeField] private SoundPlayer soundPlayer;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator drillAnimator;
    private PlayerParts parts;

    private bool drilling;
    private bool thrusting;
    private float fuel;
    private float health;
    private bool inShop;

    // Audio
    private float drillVolume = 0f;
    private float revVolume = 0f;
    private bool setHigherDrill = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        drillAnimator = drillHead.GetComponent<Animator>();
        parts = GetComponent<PlayerParts>();

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
            rb.AddForce(Vector2.up * 2.3f * parts.thrusters.Power);


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

        // Additional drilling and resource logic
        drillEffector.SetActive(drilling);
        drillAnimator.SetFloat("Speed", drillVolume * drillVolume * 1.5f);

        fuel -= drilling ? Time.deltaTime * 3f : 0;
        fuel -= thrusting ? Time.deltaTime * 3f : 0;

        fuelDisplay.SetPercent(fuel / parts.fuelTank.MaxFuel);
        fuelText.text = ((int)fuel).ToString();
        
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
            soundPlayer.PlayFail();
        }
    }

    public void RefreshFuel()
    {
        fuel = parts.fuelTank.MaxFuel;
        soundPlayer.PlayFuel();
    }
}
