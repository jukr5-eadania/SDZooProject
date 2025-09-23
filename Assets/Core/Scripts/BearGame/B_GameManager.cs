using UnityEngine;

public class B_GameManager : MonoBehaviour, IMinigame
{
    public static B_GameManager Instance { get; private set; }

    [Header("Bear Tracking")]
    [Tooltip("Total number of hidden objects")]
    [SerializeField] private int totalBears = 3;
    [Tooltip("Reference to the UI Manager that updates the score display")]
    [SerializeField] private B_UIManager uiManager;

    private int bearsFound = 0;
    public bool IsGameActive { get; private set; } = false;

    // read-only properties
    public int BearsFound => bearsFound;
    public int TotalBears => totalBears;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartGame()
    {
        bearsFound = 0;
        IsGameActive = true;

        if(uiManager != null)
        {
            uiManager.UpdateScore(bearsFound, totalBears);
        }
        BearFinder[] bearFinders = FindObjectsByType<BearFinder>(FindObjectsSortMode.None);
        foreach (var finder in bearFinders)
        {
            finder.ResetBear();
        }
    }

    public void BearFound()
    {
        bearsFound++;
        Debug.Log($"Bears found: {bearsFound} / {totalBears}");

        if (uiManager != null)
        {
            uiManager.UpdateScore(bearsFound, totalBears);
        }

        if (bearsFound == totalBears)
        {
            Debug.Log("You found all the bear cubs!");
            IsGameActive = false;
        }
    }
    

}
