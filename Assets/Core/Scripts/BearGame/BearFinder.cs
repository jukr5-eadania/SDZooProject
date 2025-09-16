using UnityEngine;

public class BearFinder : MonoBehaviour
{
    [SerializeField] private bool hasBear = false;
    [SerializeField] private GameObject bearCub;

    private bool found = false;

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
            BearGameManager.Instance.BearFound();
        }

        found = true;
    }

}
