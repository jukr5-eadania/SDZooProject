using UnityEngine;

[CreateAssetMenu(fileName = "HR_wolf_SO", menuName = "Scriptable Objects/HR_wolf_SO")]
public class HR_wolf_SO : ScriptableObject
{
    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite howlingSprite;

    [Header("Audio")]
    public AudioClip howlClip;

    [Header("Timing")]
    public float howlDuration = 1.0f;
}
