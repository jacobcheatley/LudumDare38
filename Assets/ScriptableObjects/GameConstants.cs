using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameConstants")]
public class GameConstants : ScriptableObject
{
    [Header("Level Constraints")]
    public int Width;
    public int AboveHeight;
}
