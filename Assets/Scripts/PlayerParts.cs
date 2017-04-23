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
    [SerializeField] public SoundPlayer soundPlayer;
    [SerializeField] private PlayerControl playerControl;
    

    public void Start()
    {
        Money = 0; // Broken because inspector = dumb
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
        {
            fuelTank = (FuelTank)obj;
            playerControl.RefreshFuel();
        }
        else if (obj is Thrusters)
            thrusters = (Thrusters)obj;
        else if (obj is EscapeRocket)
            Debug.Log("Win");
        UpdateColors();
    }

    public void SellOre(string ore)
    {
        switch (ore)
        {
            case "Coal":
                if (Coal > 0)
                {
                    Coal--;
                    Money += 10;
                    soundPlayer.PlaySell();
                }
                else
                    soundPlayer.PlayFail();
                break;
            case "Ruby":
                if (Ruby > 0)
                {
                    Ruby--;
                    Money += 50;
                    soundPlayer.PlaySell();
                }
                else
                    soundPlayer.PlayFail();
                break;
            case "Emerald":
                if (Emerald > 0)
                {
                    Emerald--;
                    Money += 100;
                    soundPlayer.PlaySell();
                }
                else
                    soundPlayer.PlayFail();
                break;
            case "Diamond":
                if (Diamond > 0)
                {
                    Diamond--;
                    Money += 500;
                    soundPlayer.PlaySell();
                }
                else
                    soundPlayer.PlayFail();
                break;
            case "Starstone":
                if (Starstone > 0)
                {
                    Starstone--;
                    Money += 1000;
                    soundPlayer.PlaySell();
                }
                else
                    soundPlayer.PlayFail();
                break;
        }
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
