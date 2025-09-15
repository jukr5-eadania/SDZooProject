using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class HR_GameManager : MonoBehaviour
{
    public static HR_GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float timeBetweenSteps = 1.0f;
    [SerializeField] private int maxSequenceLength = 5;

    [Header("References")]
    [SerializeField] private List<HR_wolf> wolves = new();

    private List<int> sequence = new();
    private int currentStep = 0;
    private bool playerTurn = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        sequence.Clear();
        AddStepToSequence();
        StartCoroutine(PlaySequence());
    }

    public void AddStepToSequence()
    {
        int nextWolf = Random.Range(0, wolves.Count);
        sequence.Add(nextWolf);
    }

    private System.Collections.IEnumerator PlaySequence()
    {
        playerTurn = false;

        for (int i = 0; i < sequence.Count; i++)
        {
            int wolfIndex = sequence[i];
            yield return wolves[wolfIndex].DoHowl(timeBetweenSteps);
        }

        playerTurn = true;
        currentStep = 0;
    }

    public void RegisterWolfTap(HR_wolf wolf)
    {
        if (!playerTurn) return;

        int tappedIndex = wolves.IndexOf(wolf);

        if (tappedIndex == sequence[currentStep])
        {
            currentStep++;

            if (currentStep >= sequence.Count)
            {
                AddStepToSequence();
                StartCoroutine(PlaySequence());
            }
        }
        else
        {
            Debug.Log("WRONG");
        }
    }
}
