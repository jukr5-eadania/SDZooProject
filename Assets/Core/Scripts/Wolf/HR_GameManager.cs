using UnityEngine;
using System.Collections.Generic;

public class HR_GameManager : MonoBehaviour, IMinigame
{
    public static HR_GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float timeBeforeNextRound = 2.0f;
    [SerializeField] private int maxRounds = 5;

    [Header("Wolves")]
    [SerializeField] private List<HR_wolf> wolves = new();

    [Header("UI")]
    [SerializeField] private HR_UI_Script uiScript;
    private int currentRound;

    private List<int> sequence = new();
    private int currentStep = 0;
    private bool playerTurn = false;
    public bool PlayerTurn => playerTurn;

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
        currentRound = 1;
        UpdateRoundUI();
        sequence.Clear();
        AddStepToSequence();
        StartCoroutine(PlaySequence());
    }

    public void AddStepToSequence()
    {
        if (maxRounds >= sequence.Count)
        {
            int nextWolf = Random.Range(0, wolves.Count);
            sequence.Add(nextWolf);
        }
    }

    private System.Collections.IEnumerator PlaySequence()
    {
        playerTurn = false;
        UpdateTurnUI(false);

        yield return new WaitForSeconds(timeBeforeNextRound);

        for (int i = 0; i < sequence.Count; i++)
        {
            int wolfIndex = sequence[i];
            yield return StartCoroutine(wolves[wolfIndex].DoHowl());
        }

        playerTurn = true;
        UpdateTurnUI(true);
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
        if (success)
        {
            currentRound++;
            if (currentRound > maxRounds)
            {
                Victory();
                yield break;
            }

            AddStepToSequence();
        }
        UpdateRoundUI();
        StartCoroutine(PlaySequence());

        yield return null;
    }

    private void UpdateRoundUI()
    {
        uiScript.UpdateRound(currentRound, maxRounds);
    }

    private void UpdateTurnUI(bool playerTurn)
    {
        if (playerTurn) { uiScript.UpdateTurn(true); }
        else
        {
            uiScript.UpdateTurn(false);
        }
    }

    private void Victory()
    {
        playerTurn = false;
        Debug.Log("PlayerWins");
        Minigame_UI_Script.Instance.ShowVictoryPopup();
    }
}
