using UnityEngine;

public class PlayerParts : MonoBehaviour
{
    public DrillHead drillHead;
    public FuelTank fuelTank;
    public Thrusters thrusters;

    public int Coal;
    public int Ruby;
    public int Emerald;
    public int Diamond;
    public int Starstone;
    public int Money;

    [SerializeField] private SpriteRenderer drillRenderer;
    [SerializeField] private SpriteRenderer diggerRenderer;
    

    public void Start()
    {
        UpdateColors();
    }

    public void UpdateColors()
    {
        drillRenderer.color = drillHead.ObjectColor();
        diggerRenderer.color = fuelTank.ObjectColor();
    }

    public void AddPart(FullDescObject obj)
    {
        if (obj is DrillHead)
            drillHead = (DrillHead)obj;
        else if (obj is FuelTank)
            fuelTank = (FuelTank)obj;
        else if (obj is Thrusters)
            thrusters = (Thrusters)obj;
        else if (obj is EscapeRocket)
            Debug.Log("Win");
        UpdateColors();
    }
}
