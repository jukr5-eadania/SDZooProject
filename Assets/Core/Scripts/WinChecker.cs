using UnityEngine;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SaveSO saveSO;
    [SerializeField] private string trophyName;
    private bool trophyGotten;

    private Image img;
    void Start()
    {
        if (saveSO.saveData.TryGetValue(trophyName, out bool value))
        {
            trophyGotten = value;
        }
        else
        {
            saveSO.saveData.Add(trophyName, false);
        }

        img = GetComponent<Image>();
        if (trophyGotten)
        {
            img.color = Color.white;
        }
        else
        {
            img.color = Color.black;
        }
    }
}
