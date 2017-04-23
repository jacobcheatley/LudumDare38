using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Drill Parts")]
    [SerializeField] private GameObject drillAxis;
    [SerializeField] private GameObject drillEffector;
    [SerializeField] private GameObject drillHead;

    [Header("UI")]
    [SerializeField] private DisplaySlider fuelSlider;
    [SerializeField] private Text fuelText;
    [SerializeField] private GameObject shopCanvas;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Animator drillAnimator;
    private PlayerParts parts;

    private bool drilling;
    private bool thrusting;
    private float fuel;
    private float health;
    private bool inShop;

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
            rb.AddForce(Vector2.up * 2.5f * parts.thrusters.Power);


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

        // Additional drilling and resource logic
        drillEffector.SetActive(drilling);
        drillAnimator.SetFloat("Speed", drilling ? 1 : 0);

        fuel -= drilling ? Time.deltaTime * 5f : 0;
        fuel -= thrusting ? Time.deltaTime * 4f : 0;

        fuelSlider.SetSize(fuel / parts.fuelTank.MaxFuel);
        fuelText.text = ((int)fuel).ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shop")
        {
            inShop = true;
            shopCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Shop")
        {
            inShop = false;
            shopCanvas.SetActive(false);
        }
    }
}
