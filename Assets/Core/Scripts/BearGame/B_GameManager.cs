using UnityEngine;

public class B_GameManager : MonoBehaviour
{
    public static B_GameManager Instance { get; private set; }

    [Header("Bear Tracking")]
    [Tooltip("Total number of hidden objects")]
    [SerializeField] private int totalBears = 3;
    [Tooltip("Reference to the UI Manager that updates the score display")]
    [SerializeField] private B_UIManager uIManager;

    private int bearsFound = 0;

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

    public void BearFound()
    {
        bearsFound++;
        Debug.Log($"Bears found: {bearsFound} / {totalBears}");

        if (uIManager != null)
        {
            uIManager.UpdateScore(bearsFound, totalBears);
        }

        if (bearsFound == totalBears)
        {
            Debug.Log("You found all the bear cubs!");
        }
    }
}
