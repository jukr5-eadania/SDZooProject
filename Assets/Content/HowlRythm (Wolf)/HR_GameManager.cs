using UnityEngine;
using System.Collections.Generic;

public class HR_GameManager : MonoBehaviour
{
    public static HR_GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float timeBetweenHowl = 2.0f;
    [SerializeField] private float timeBeforeNextRound = 2.0f;
    [SerializeField] private int maxSequenceLength = 5;

    [Header("Wolves")]
    [SerializeField] private List<HR_wolf> wolves = new();

    [Header("UI")]
    [SerializeField] private HR_UI_Script uiScript;
    private int currentRound = 1;

    private List<int> sequence = new();
    private int currentStep = 0;
    private bool playerTurn = false;
    public bool PlayerTurn => playerTurn;

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
        UpdateUI();
        sequence.Clear();
        AddStepToSequence();
        StartCoroutine(PlaySequence());
    }

    public void AddStepToSequence()
    {
        if (maxSequenceLength >= sequence.Count)
        {
            int nextWolf = Random.Range(0, wolves.Count);
            sequence.Add(nextWolf);
        }
    }

    private System.Collections.IEnumerator PlaySequence()
    {
        playerTurn = false;

        for (int i = 0; i < sequence.Count; i++)
        {
            int wolfIndex = sequence[i];
            yield return StartCoroutine(wolves[wolfIndex].DoHowl());
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
                playerTurn = false;
                StartCoroutine(StartNextRound(true));
            }
        }
        else
        {
            Debug.Log("WRONG");
            wolf.onWolfFail?.Invoke();
            StartCoroutine(StartNextRound(false));
        }
    }

    private System.Collections.IEnumerator StartNextRound(bool success)
    {
        yield return new WaitForSeconds(timeBeforeNextRound);
        if (success)
        {
            AddStepToSequence();
            currentRound++;
        }
        UpdateUI();
        StartCoroutine(PlaySequence());
    }

    private void UpdateUI()
    {
        uiScript.UpdateRound(currentRound, maxSequenceLength);
    }
}
