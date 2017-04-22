using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Cost
{
    public int Coal, Ruby, Emerald, Diamond, Starstone, Money;

    private static List<string> BudgetZip(int[] values, string[] names)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] != 0)
                result.Add(string.Format("{0} {1}", values[i], names[i]));
        }
        return result;
    }

    public override string ToString()
    {
        int[] values = {Coal, Ruby, Emerald, Diamond, Starstone, Money};
        List<string> zippedList = BudgetZip(values, new string[] { "Coal", "Ruby", "Emerald", "Diamond", "Starstone", "Money" });
        return string.Join(",", zippedList.ToArray());
    }
}

[System.Serializable]
public class ShopItem
{
    public FullDescObject Obj;
    public Cost Cost;
}

public class Shop : MonoBehaviour
{
    [Header("Prefabs and Object References")]
    [SerializeField] private GameObject shopButtonPrefab;
    [SerializeField] private PlayerParts parts;

    [Header("Left Pane")]
    [SerializeField] private Transform drillHeadPlace;
    [SerializeField] private Transform fuelTankPlace;
    [SerializeField] private Transform thrustersPlace;
    [SerializeField] private Transform escapeRocketPlace;

    [SerializeField] private Sprite drillSprite;
    [SerializeField] private Sprite fuelTankSprite;
    [SerializeField] private Sprite thrustersSprite;
    [SerializeField] private Sprite escapeRocketSprite;

    [SerializeField] private Text drillName;
    [SerializeField] private Text drillDesc;
    [SerializeField] private Text fuelTankName;
    [SerializeField] private Text fuelTankDesc;
    [SerializeField] private Text thrustersName;
    [SerializeField] private Text thrustersDesc;
    [SerializeField] private Text escapeRocketName;
    [SerializeField] private Text escapeRocketDesc;

    [Header("Left Pane Items")]
    [SerializeField] private ShopItem[] drillHeads;
    [SerializeField] private ShopItem[] fuelTanks;
    [SerializeField] private ShopItem[] thrusters;
    [SerializeField] private ShopItem escapeRocket;

    private int drillIndex = 0;
    private int fuelIndex = 0;
    private int thrustersIndex = 0;

    void Start()
    {
        UpdateDescriptions();
        PlaceShopButton(drillHeadPlace, drillSprite, drillHeads[drillIndex]);
        PlaceShopButton(fuelTankPlace, fuelTankSprite, fuelTanks[fuelIndex]);
        PlaceShopButton(thrustersPlace, thrustersSprite, thrusters[thrustersIndex]);
        PlaceShopButton(escapeRocketPlace, escapeRocketSprite, escapeRocket);
    }

    void PlaceShopButton(Transform place, Sprite sprite, ShopItem item)
    {
        GameObject button = Instantiate(shopButtonPrefab, place.position, Quaternion.identity, gameObject.transform);
        button.GetComponent<ShopButton>().Initialise(item, sprite, parts, () => Destroy(button));
    }

    public void UpdateDescriptions()
    {
        UpdateItem(drillHeads[drillIndex], drillName, drillDesc);
        UpdateItem(fuelTanks[fuelIndex], fuelTankName, fuelTankDesc);
        UpdateItem(thrusters[thrustersIndex], thrustersName, thrustersDesc);
        UpdateItem(escapeRocket, escapeRocketName, escapeRocketDesc);
    }

    private void UpdateItem(ShopItem item, Text name, Text desc)
    {
        name.text = item.Obj.Name;
        desc.text = string.Format("{0}\n{1}", item.Cost.ToString(), item.Obj.FullDesc());
    }
}
