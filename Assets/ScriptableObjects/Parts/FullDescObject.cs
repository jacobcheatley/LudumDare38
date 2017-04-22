using UnityEngine;

public abstract class FullDescObject : ScriptableObject
{
    public string Name;
    public abstract Color ObjectColor();
    public abstract string FullDesc();
}
