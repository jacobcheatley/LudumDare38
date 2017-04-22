using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [HideInInspector]
    public FullDescObject Obj;
    [HideInInspector]
    public Cost Cost;

    private PlayerParts parts;
    private Action function;

    public void Initialise(ShopItem item, Sprite sprite, PlayerParts parts, Action finishedFunction)
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
        image.color = item.Obj.ObjectColor();

        Obj = item.Obj;
        Cost = item.Cost;
        this.parts = parts;
        function = finishedFunction;
    }

    public void Clicked()
    {
        if (Cost.CanAfford(parts))
        {
            parts.AddPart(Obj);
            function();
        }
        else
        {
            // TODO: Error display
            Debug.Log("Nuk g");
        }
    }
}
