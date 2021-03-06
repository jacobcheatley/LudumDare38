﻿using UnityEngine;
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
    [SerializeField] public PlayerControl playerControl;
    [SerializeField] private ParticleSystem system;
    [SerializeField] public GameObject errorPrefab;

    public void Start()
    {
        Money = 0; // Broken because inspector = dumb
        UpdateColors();
    }

    void Update()
    {
        resourceText.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", Coal, Ruby, Emerald, Diamond, Starstone, Money);
    }

    public void Emission()
    {
        system.Emit(Random.value < 0.25f ? 1 : 0);
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
            playerControl.Win();
        UpdateColors();
    }

    public void SellOre(string ore)
    {
        bool success = false;
        switch (ore)
        {
            case "Coal":
                if (Coal > 0)
                {
                    Coal--;
                    Money += 10;
                    success = true;
                }
                break;
            case "Ruby":
                if (Ruby > 0)
                {
                    Ruby--;
                    Money += 50;
                    success = true;
                }
                break;
            case "Emerald":
                if (Emerald > 0)
                {
                    Emerald--;
                    Money += 100;
                    success = true;
                }
                break;
            case "Diamond":
                if (Diamond > 0)
                {
                    Diamond--;
                    Money += 500;
                    success = true;
                }
                break;
            case "Starstone":
                if (Starstone > 0)
                {
                    Starstone--;
                    Money += 1000;
                    success = true;
                }
                break;
        }
        if (success)
        {
            soundPlayer.PlaySell();
        }
        else
        {
            GameObject error = Instantiate(errorPrefab, Vector3.one * 256, Quaternion.identity, playerControl.shopCanvas.transform);
            error.GetComponent<ErrorText>().Init("Don't have that ore.");
            soundPlayer.PlayFail();
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

    public void UpdateParticleColors(ParticleSystem.MinMaxGradient gradient)
    {
        var test = system.main;
        test.startColor = gradient;
        system.Play();
    }
}
