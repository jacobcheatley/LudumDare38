using UnityEngine;
using UnityEngine.UI;

public class RefuelTracker : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private PlayerControl control;

    void Update()
    {
        text.text = string.Format("Refuel (${0})", control.RefuelCost());
    }
}
