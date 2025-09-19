using UnityEngine;
using System.Collections;

public class BearFinder : MonoBehaviour
{
    [SerializeField] private bool hasBear = false;
    [SerializeField] private GameObject bearCub;
    [SerializeField] private HiddenObjectData_SO objectData;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController baseController;
    [SerializeField] private AudioSource audioSource;

    private bool found = false;

    private void Start()
    {
        if(hasBear && !found && objectData != null && animator != null)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(baseController);

            overrideController["Hint_anim"] = objectData.hintAnimation;
            overrideController["Idle_anim"] = objectData.idleAnimation;

            animator.runtimeAnimatorController = overrideController;
            StartCoroutine(PlayHintLoop());            
        }

    }

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
            BearGameManager.Instance.BearFound();
        }

        
    }

}
