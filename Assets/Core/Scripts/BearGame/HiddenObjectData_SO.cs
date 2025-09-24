using UnityEngine;

[CreateAssetMenu(fileName = "HiddenObjectData_SO", menuName = "Scriptable Objects/HiddenObjectData_SO")]
public class HiddenObjectData_SO : ScriptableObject
{
    [Header("Animation Clips")]
    [Tooltip("Idle animation played when the object is not hinting")]
    public AnimationClip idleAnimation;
    [Tooltip("Hint animation played to attract the player's attention")]
    public AnimationClip hintAnimation;

    [Header("Timing Setting")]
    [Tooltip("Duration of the hint animation in seconds")]
    public float hintDuration = 1.5f;
    [Tooltip("Time between hint animations in seconds")]
    public float timeBetweenHint = 2f;

    [Header("Audio Clips")]
    [Tooltip("Sound played during the hint animation")]
    public AudioClip hintSound;
    [Tooltip("Sound played when the object is found")]
    public AudioClip foundSound;
}
