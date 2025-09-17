using UnityEngine;

[CreateAssetMenu(fileName = "HiddenObjectData_SO", menuName = "Scriptable Objects/HiddenObjectData_SO")]
public class HiddenObjectData_SO : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite hintSprite;

    [Header("Timing")]
    [SerializeField] private float hintDuration = 1.5f;

    [Header("Audio")]
    [SerializeField] private AudioClip hintSound;
}
