using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DrillHead")]
public class DrillHead : FullDescObject
{
    public Color DrillColor;
    public float Speed;
    public int Power;
    public string Description;

    public override string FullDesc()
    {
        return string.Format("{0}x Speed\n{1}", Speed, Description);
    }

    public override Color ObjectColor()
    {
        return DrillColor;
    }
}
