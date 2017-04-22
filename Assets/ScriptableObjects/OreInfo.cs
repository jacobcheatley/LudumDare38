using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ore")]
public class OreInfo : ScriptableObject
{
    public Sprite Sprite;
    public int Value;
    public string Name;
    public GameObject OrePrefab;
}

[System.Serializable]
public class OreRoll
{
    public OreInfo Info;
    public float Value;
}
