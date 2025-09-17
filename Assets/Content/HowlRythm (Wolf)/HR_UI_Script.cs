using UnityEngine;
using UnityEngine.UIElements;

public class HR_UI_Script : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private Label roundLabel;
    private Label turnLabel;

    private VisualElement overlay;
    private Button startButton;

    private void Awake()
    {
        var root = uiDocument.rootVisualElement;

        roundLabel = root.Q<Label>("RoundLabel");
        turnLabel = root.Q<Label>("TurnLabel");

        overlay = root.Q<VisualElement>("Overlay");
        startButton = root.Q<Button>("StartButton");
        startButton.clicked += OnStartButtonClicked;

        //overlay.style.display = DisplayStyle.None;
    }

    public void ShowRulesPopup() => overlay.style.display = DisplayStyle.Flex;
    public void HideRulesPopUp() => overlay.style.display = DisplayStyle.None;

    private void OnStartButtonClicked()
    {
        HideRulesPopUp();
        HR_GameManager.Instance.StartGame();
    }

    public void UpdateRound(int currentRound, int maxRounds)
    {
        if (roundLabel != null)
        {
            roundLabel.text = $"Runde {currentRound} ud af {maxRounds}";
        }
        else { Debug.Log("Missing Roundlabel"); }
    }

    public void UpdateTurn(bool playerTurn)
    {
        if (turnLabel != null)
        {
            if (playerTurn)
            {
                turnLabel.text = "Det er din tur! Gentag rækkefølgen ulvene hylede i";
            }
            else { turnLabel.text = "Hold godt øje med rækkefølgen ulvene hyler i"; }
        }
        else { Debug.Log("Missing TurnLabel"); }
    }
}
