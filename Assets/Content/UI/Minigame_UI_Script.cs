using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Minigame_UI_Script : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private Minigame_UI_Data data;

    private Button homeButton;
    private Button howToPlayButton;
    private VisualElement howToPlayPopup;
    private VisualElement victoryPopup;
    private Button victoryHomeButton;

    private Image stickerImage;
    private Label victoryText;
    private Label howToPlayText;

    private void Awake()
    {
        var root = uiDocument.rootVisualElement;

        homeButton = root.Q<Button>("HomeButton");
        howToPlayButton = root.Q<Button>("HowToPlayButton");
        howToPlayPopup = root.Q<VisualElement>("HowToPlayPopup");
        victoryPopup = root.Q<VisualElement>("VictoryPopup");
        victoryHomeButton = root.Q<Button>("VictoryHomeButton");

        homeButton.clicked += OnHomeClicked;
        howToPlayButton.clicked += ShowHowToPlay;
        howToPlayPopup.Q<Button>("CloseButton").clicked += HideHowToPlay;
        victoryHomeButton.clicked += OnHomeClicked;

        victoryPopup.style.display = DisplayStyle.None;

        SetVictoryContent(data.victorySticker, data.victoryText);
        howToPlayText.text = data.howToPlayText;
    }

    private void OnDestroy()
    {
        homeButton.clicked -= OnHomeClicked;
        howToPlayButton.clicked -= ShowHowToPlay;
        howToPlayPopup.Q<Button>("CloseButton").clicked -= HideHowToPlay;
        victoryHomeButton.clicked -= OnHomeClicked;
    }

    private void OnHomeClicked()
    {
        Debug.Log("Home Clicked");
        //SceneManager.LoadScene("");
    }

    private void ShowHowToPlay()
    {
        howToPlayPopup.style.display = DisplayStyle.Flex;
    }

    private void HideHowToPlay()
    {
        howToPlayPopup.style.display = DisplayStyle.None;
    }

    private void ShowVictoryPopup()
    {
        victoryPopup.style.display = DisplayStyle.Flex;
    }

    private void HideVictoryPopup()
    {
        victoryPopup.style.display = DisplayStyle.None;
    }

    public void SetVictoryContent(Sprite image, string text)
    {
        stickerImage.sprite = image;
        victoryText.text = text;
    }
}
