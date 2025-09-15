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
        if (found) return;

        if(hasBear == true && bearCub != null)
        {
            bearCub.SetActive(true);
            BearGameManager.Instance.BearFound();
        }

        found = true;
    }

    private void OnMouseDown()
    {
        Debug.Log("Busken bliver klikket");
        OnFound();
    }
}
