using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [HideInInspector]
    public FullDescObject Obj;
    [HideInInspector]
    public Cost Cost;

    [SerializeField] private GameObject errorPrefab;

    private PlayerParts parts;
    private Action function;
    private SoundPlayer soundPlayer;

    public void Initialise(ShopItem item, Sprite sprite, PlayerParts parts, Action finishedFunction)
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
        image.color = item.Obj.ObjectColor();

        soundPlayer = FindObjectOfType<SoundPlayer>();

        Obj = item.Obj;
        Cost = item.Cost;
        this.parts = parts;
        function = finishedFunction;
    }

    public void Clicked()
    {
        if (Cost.CanAfford(parts))
        {
            soundPlayer.PlayBuy();
            parts.AddPart(Obj);
            Cost.Charge(parts);
            function();
        }
        else
        {
            GameObject error = Instantiate(errorPrefab, Vector3.one * 256, Quaternion.identity, gameObject.transform.parent);
            error.GetComponent<ErrorText>().Init("Can't afford that item.");
            soundPlayer.PlayFail();
        }
    }
}
