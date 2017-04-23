using UnityEngine;
using UnityEngine.UI;

public class PlayerParts : MonoBehaviour
{
    public DrillHead drillHead;
    public FuelTank fuelTank;
    public Thrusters thrusters;

    [HideInInspector] public int Coal;
    [HideInInspector] public int Ruby;
    [HideInInspector] public int Emerald;
    [HideInInspector] public int Diamond;
    [HideInInspector] public int Starstone;
    [HideInInspector] public int Money;

    [SerializeField] private SpriteRenderer drillRenderer;
    [SerializeField] private SpriteRenderer diggerRenderer;
    [SerializeField] private Text resourceText;
    

    public void Start()
    {
        Money = 9000; // Broken because inspector = dumb
        UpdateColors();
    }

    void Update()
    {
        resourceText.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", Coal, Ruby, Emerald, Diamond, Starstone, Money);
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

    public void AddOre(OreInfo info)
    {
        // This code is bad and I should feel bad
        switch (info.Name)
        {
            case "Coal":
                Coal++;
                break;
            case "Ruby":
                Ruby++;
                break;
            case "Emerald":
                Emerald++;
                break;
            case "Diamond":
                Diamond++;
                break;
            case "Starstone":
                Starstone++;
                break;
        }
    }
}
