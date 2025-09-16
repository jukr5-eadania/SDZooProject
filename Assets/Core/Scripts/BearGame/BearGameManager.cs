using UnityEngine;

public class BearGameManager : MonoBehaviour
{
    public static BearGameManager Instance { get; private set; }

    [SerializeField] private int totalBears = 3;

    private int bearsFound = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
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

        if(bearsFound == totalBears)
        {
            Debug.Log("You found all the bear cubs!");
        }
    }
}
