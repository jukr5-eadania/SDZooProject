using UnityEngine;

[CreateAssetMenu(fileName = "HiddenObjectData_SO", menuName = "Scriptable Objects/HiddenObjectData_SO")]
public class HiddenObjectData_SO : ScriptableObject
{
    [Header("Animation")]
    public AnimationClip idleAnimation;
    public AnimationClip hintAnimation;

    [Header("Timing")]
    public float hintDuration = 1.5f;
    public float timeBetweenHint = 2f;

    [Header("Audio")]
    public AudioClip hintSound;
}
