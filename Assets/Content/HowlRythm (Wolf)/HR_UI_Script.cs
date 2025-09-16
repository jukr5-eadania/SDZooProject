using UnityEngine;
using UnityEngine.UIElements;

public class HR_UI_Script : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private Label roundLabel;

    private void Awake()
    {
        var root = uiDocument.rootVisualElement;
        roundLabel = root.Q<Label>("RoundLabel");
    }

    public void UpdateRound(int currentRound, int maxRounds)
    {
        if (roundLabel != null)
        {
            roundLabel.text = $"Round {currentRound} / {maxRounds}";
        }
        else { Debug.Log("Missing ROundlabel"); }
    }
}
