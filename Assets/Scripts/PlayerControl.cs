using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject drillAxis;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("HorizontalMove");
        rb.AddForce(Vector2.right * horizontal);

        float vertical = Input.GetAxisRaw("VerticalMove");
        if (vertical > 0)
            rb.AddForce(Vector2.up * 2);

        // Drill Rotation
        float verticalDrill = Input.GetAxisRaw("VerticalDrill");
        float horizontalDrill = Input.GetAxisRaw("HorizontalDrill");
        if (verticalDrill != 0 || horizontalDrill != 0)
        {
            float newAngle = Mathf.Atan2(verticalDrill, horizontalDrill) * Mathf.Rad2Deg;
            float currentAngle = drillAxis.transform.rotation.eulerAngles.z;
            Debug.Log(newAngle);
            drillAxis.transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, newAngle, Time.deltaTime * 20f), Vector3.forward);
        }
    }
}
