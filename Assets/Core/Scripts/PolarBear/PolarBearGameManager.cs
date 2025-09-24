using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PolarBearGameManager : MonoBehaviour, IMinigame
{
    public static PolarBearGameManager Instance { get; private set; }

    private bool started = false;
    [SerializeField] private GameObject fishingHoleList;
    private List<GameObject> fishingHoles = new List<GameObject>();
    private List<GameObject> activatedfishingHoles = new List<GameObject>();

    [Header("Game Settings")]
    private float timer = 0;
    [SerializeField]private float timerLength = 1;
    private int fishCurrent = 0;
    [SerializeField]private int fishGoal = 20;

    [Header("UI Settings")]
    [SerializeField] UIDocument PolarBearUIObject;
    private Label scoreLabel;

    [Header("Save Settings")]
    [SerializeField] private SaveSO saveSO;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        var root = PolarBearUIObject.rootVisualElement;
        scoreLabel = root.Q<Label>("Score");
        scoreLabel.text = $"Score 0 / {fishGoal}";

        foreach (Transform child in fishingHoleList.transform)
        {
            fishingHoles.Add(child.gameObject);
        }
        foreach (GameObject hole in fishingHoles)
        {
            hole.SendMessage("deactivateFish", SendMessageOptions.DontRequireReceiver);
        }
    }

    void Update()
    {
        if (started)
        {
            timer += Time.deltaTime;
            if (timer >= timerLength)
            {
                foreach (GameObject hole in activatedfishingHoles)
                {
                    hole.SendMessage("deactivateFish", SendMessageOptions.DontRequireReceiver);
                }
                foreach (GameObject hole in fishingHoles)
                {
                    if (Random.Range(0, fishingHoles.Count - 1) == 0)
                    {
                        hole.SendMessage("activateFish", SendMessageOptions.DontRequireReceiver);
                        activatedfishingHoles.Add(hole);
                    }
                }
                timer = 0;
            } 
        }

    }
    void IncreaseScore()
    {
        fishCurrent++;

        if (fishCurrent >= fishGoal)
        {
            fishCurrent = fishGoal;
            started = false;
            Minigame_UI_Script.Instance.ShowVictoryPopup();
            if (saveSO.saveData.TryGetValue("PolarBear", out bool value))
            {
                saveSO.saveData["PolarBear"] = true;
            }
        }
        scoreLabel.text = $"Score {fishCurrent} / {fishGoal}";
    }

    public void StartGame()
    {
        started = true;
        fishCurrent = 0;
    }
}
