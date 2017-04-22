using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/FuelTank")]
public class FuelTank : FullDescObject
{
    public Color TankColor;
    public float MaxFuel;
    public string Description;

    public override string FullDesc()
    {
        return string.Format("{0} Max Fuel\n{1}", MaxFuel, Description);
    }

    public override Color ObjectColor()
    {
        return TankColor;
    }
}
