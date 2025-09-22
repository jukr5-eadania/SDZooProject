using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Minigame_UI_Script : MonoBehaviour
{
    public static Minigame_UI_Script Instance { get; private set; }

    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private Minigame_UI_Data data;
    [SerializeField] private MonoBehaviour minigame;

    private IMinigame activeMinigame;

    private Button homeButton;

    private Button howToPlayButton;
    private VisualElement howToPlayPopup;
    private Label howToPlayText;
    private Button startButton;

    private VisualElement victoryPopup;
    private VisualElement stickerImage;
    private Button victoryHomeButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        var root = uiDocument.rootVisualElement;

        homeButton = root.Q<Button>("HomeButton");

        howToPlayButton = root.Q<Button>("HowToPlayButton");
        howToPlayPopup = root.Q<VisualElement>("HowToPlayPopup");
        howToPlayText = root.Q<Label>("HowToPlayText");
        startButton = root.Q<Button>("StartButton");

        victoryPopup = root.Q<VisualElement>("VictoryPopup");
        stickerImage = root.Q<VisualElement>("StickerImage");
        victoryHomeButton = root.Q<Button>("VictoryHomeButton");

        homeButton.clicked += OnHomeClicked;

        howToPlayButton.clicked += ShowHowToPlay;
        startButton.clicked += OnStartClicked;

        victoryHomeButton.clicked += OnHomeClicked;

        victoryPopup.style.display = DisplayStyle.None;

        stickerImage.style.backgroundImage = new StyleBackground(data.victorySticker);
        howToPlayText.text = data.howToPlayText;
    }

    private void Start()
    {
        if (minigame is IMinigame mg)
        {
            activeMinigame = mg;
        }
        Debug.Log("Minigame assigned: " + minigame);
    }

    private void OnDestroy()
    {
        homeButton.clicked -= OnHomeClicked;
        howToPlayButton.clicked -= ShowHowToPlay;
        startButton.clicked -= OnStartClicked;
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

    private void OnStartClicked()
    {
        howToPlayPopup.style.display = DisplayStyle.None;
        activeMinigame.StartGame();
    }

    public void ShowVictoryPopup()
    {
        victoryPopup.style.display = DisplayStyle.Flex;
    }
}
