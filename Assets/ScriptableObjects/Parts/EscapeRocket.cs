using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EscapeRocket")]
public class EscapeRocket : FullDescObject
{
    public override string FullDesc()
    {
        return "Game winning rocket\nmade of starstone.";
    }

    public override Color ObjectColor()
    {
        return Color.white;
    }
}
