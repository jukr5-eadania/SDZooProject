using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SaveSO saveSO;
    [SerializeField] private string trophyName;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (saveSO.saveData.TryGetValue(trophyName, out bool value))
        {
            saveSO.saveData[trophyName] = true;
        }
    }
}
