using UnityEngine;

public class RandomiseOrientation : MonoBehaviour
{
    void Start()
    {
        float angle = Random.Range(0, 3) * 90f;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
