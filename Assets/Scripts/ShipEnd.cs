using UnityEngine;

public class ShipEnd : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(-Time.deltaTime / 2f, Vector3.forward);
        transform.position += transform.up * Time.deltaTime * 8f;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.1f, Time.deltaTime * 0.01f);
    }
}
