using UnityEngine;

public class PlayerParts : MonoBehaviour
{
    public DrillHead drillHead;
    public FuelTank fuelTank;
    public Plating plating;
    public Thrusters thrusters;

    [SerializeField] private GameObject drillHeadObject;

    private SpriteRenderer diggerRenderer;
    private SpriteRenderer drillRenderer;

    public void Start()
    {
        diggerRenderer = GetComponent<SpriteRenderer>();
        drillRenderer = drillHeadObject.GetComponent<SpriteRenderer>();
        UpdateColors();
    }

    public void UpdateColors()
    {
        diggerRenderer.color = plating.PlateColor;
        drillRenderer.color = drillHead.DrillColor;
    }
}
