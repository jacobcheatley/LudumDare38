using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 prevMousePosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float verticalDrill = Input.GetAxisRaw("VerticalDrill");
        float horizontalDrill = Input.GetAxisRaw("HorizontalDrill");
        float currentAngle = transform.rotation.eulerAngles.z;
        float newAngle = currentAngle;

        if (verticalDrill != 0 || horizontalDrill != 0)
            newAngle = Mathf.Atan2(verticalDrill, horizontalDrill) * Mathf.Rad2Deg;
        else if (Input.mousePosition != prevMousePosition)
        {
            prevMousePosition = Input.mousePosition;
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newAngle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, newAngle, Time.deltaTime * 20f), Vector3.forward);
    }
}
