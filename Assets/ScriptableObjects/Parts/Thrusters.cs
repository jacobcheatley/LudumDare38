using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Thrusters")]
public class Thrusters : FullDescObject
{
    public Color ThrustColor;
    public float Power;
    public string Description;

    public override string FullDesc()
    {
        return string.Format("{0}x Power\n{1}", Power, Description);
    }

    public override Color ObjectColor()
    {
        return ThrustColor;
    }
}
