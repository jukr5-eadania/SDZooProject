using UnityEngine;
using UnityEngine.UIElements;

public class B_UIManager : MonoBehaviour
{
    private Label scoreLabel;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");
               
        if (B_GameManager.Instance != null)
        {
            UpdateScore(B_GameManager.Instance.BearsFound, B_GameManager.Instance.TotalBears);
        }

    }

    public void UpdateScore(int found, int total)
    {
        Debug.Log("UpdateScore !!");
        if (scoreLabel != null)
        {
            Debug.Log("fundet: " + found);
            scoreLabel.text = $"fundet {found} ud af {total}";
        }
    }
}
