using UnityEngine;
using UnityEngine.UIElements;

public class BearUIManager : MonoBehaviour
{
    private Label scoreLabel;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");

        if(BearGameManager.Instance != null)
        {
            UpdateScore(BearGameManager.Instance.BearsFound, BearGameManager.Instance.TotalBears);
        }
        
    }

    public void UpdateScore(int found, int total)
    {
        if (scoreLabel != null)
        {
            //scoreLabel.text = $"Fundet {found} ud af {total}";

            string template = scoreLabel.text;
            template = template.Replace("{found}", found.ToString());
            template = template.Replace("{total}", total.ToString());
            scoreLabel.text = template;
        }
    }
}
