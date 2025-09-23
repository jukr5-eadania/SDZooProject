using UnityEngine;
using UnityEngine.UIElements;

public class B_UIManager : MonoBehaviour
{
    private Label scoreLabel;
    private string scoreTemplate;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");


        if (scoreLabel != null) { scoreTemplate = scoreLabel.text; }  


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
            //string template = scoreLabel.text;
            //template = template.Replace("{found}", found.ToString());
            //template = template.Replace("{total}", total.ToString());
            //scoreLabel.text = template;

            string text = scoreTemplate.Replace("{found}", found.ToString()).Replace("{total}", total.ToString());
            scoreLabel.text = text;
        }
    }
}
