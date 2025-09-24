using UnityEngine;

[CreateAssetMenu(fileName = "Minigame_UI_Data", menuName = "Scriptable Objects/Minigame_UI_Data")]
public class Minigame_UI_Data : ScriptableObject
{
    public Sprite victorySticker;

    [TextArea] public string howToPlayText;
}
