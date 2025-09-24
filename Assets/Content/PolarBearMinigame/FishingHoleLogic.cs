using UnityEngine;
using UnityEngine.EventSystems;

public class FishingHoleLogic : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject Fish;
    [SerializeField] private GameObject Hole;

    private bool active;
    
    void Start()
    {

    }

    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
        {
            Fish.SetActive(false);
            PolarBearGameManager.Instance.SendMessage("IncreaseScore", SendMessageOptions.DontRequireReceiver);
            PolarBearLogic.Instance.SendMessage("Eat", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void deactivateFish()
    {
        active = false;
        Fish.SetActive(false);
    }
    public void activateFish()
    {
        active = true;
        Fish.SetActive(true);
    }
}
