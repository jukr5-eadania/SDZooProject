using UnityEngine;

public class FishingHoleLogic : MonoBehaviour
{
    [SerializeField] private GameObject Fish;
    [SerializeField] private GameObject Hole;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void deactivateFish()
    {
        Fish.SetActive(false);
    }
    public void activateFish()
    {
        Fish.SetActive(true);
    }
}
