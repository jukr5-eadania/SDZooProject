using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BearFinder : MonoBehaviour, IPointerDownHandler
{
    [Header("Settings")]
    [Tooltip("Set to true if this object is holding a hidden object")]
    [SerializeField] private bool hasBear = false;

    [Tooltip("GameObject that represents the hidden object")]
    [SerializeField] private GameObject bearCub;

    [Header("Object Data")]
    [Tooltip("Scriptable Object with the animations and sound effects for this object")]
    [SerializeField] private HiddenObjectData_SO objectData;

    [Header("Animation Settings")]
    [Tooltip("Animator component that controls the animations")]
    [SerializeField] private Animator animator;

    [Tooltip("Base animator controller that gets overridden")]
    [SerializeField] private RuntimeAnimatorController baseController;

    [Header("Sound Effects")]
    [Tooltip("Audio Source that plays hint- and found-sound effects")]
    [SerializeField] private AudioSource audioSource;

    private bool found = false;

    private IEnumerator PlayHintLoop()
    {
        while (!found)
        {
            animator.Play("Hint_anim");

            if (objectData.hintSound != null && audioSource != null)
            {
                Debug.Log("plays sound: "+ objectData.hintSound.name);
                audioSource.PlayOneShot(objectData.hintSound);
            }

            // wait to the animation is done
            yield return new WaitForSeconds(objectData.hintDuration);
            //change to idle animation
            animator.Play("Idle_anim");
            //wait for next hint
            yield return new WaitForSeconds(objectData.timeBetweenHint);
        }
    }

    /// <summary>
    /// Called when player clicks/tabs the object
    /// </summary>
    public void OnFound()
    {
        if (!B_GameManager.Instance.IsGameActive) {  return; }

        Debug.Log("The bush was clicked");
        if (found)
        {
            Debug.Log("A bear her has already been found");
            return;
        }

        if(hasBear == true && bearCub != null)
        {
            bearCub.SetActive(true);
            
            if(objectData.foundSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(objectData.foundSound);
            }

            found = true;
            animator.Play("Idle_anim");            
            B_GameManager.Instance.BearFound();
        }
                
    }

    public void ResetBear()
    {
        found = false;
        if(bearCub != null) { bearCub.SetActive(false); }
        if(hasBear && objectData != null && animator != null)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(baseController);

            overrideController["Hint_anim"] = objectData.hintAnimation;
            overrideController["Idle_anim"] = objectData.idleAnimation;

            animator.runtimeAnimatorController = overrideController;
            StartCoroutine(PlayHintLoop());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Runs OnPointerDown");
        OnFound();
    }
}
