using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HawkGameManager : MonoBehaviour, IMinigame
{
    [Header("Game Settings")]
    [SerializeField] float timeBetweenFish = 5;
    float timer = 0;
    [SerializeField] GameObject fishPrefab;
    private bool started = false;
    private int fishCurrent = 0;
    [SerializeField] private int fishGoal = 20;

    [Header("UI Settings")]
    private Label scoreLabel;
    [SerializeField] UIDocument HawkUIObject;
    [SerializeField] UIDocument MinigameUI;

    [Header("Save Settings")]
    [SerializeField] private SaveSO saveSO;



    void Start()
    {
        var root = HawkUIObject.rootVisualElement;
        scoreLabel = root.Q<Label>("Score");
        scoreLabel.text = $"Score 0 / {fishGoal}";
    }
    public void StartGame()
    {
        Instantiate(fishPrefab);
        fishCurrent = 0;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (started)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFish)
            {
                Instantiate(fishPrefab);
                timer = 0;
            } 
        }
    }

    void IncreaseScore()
    {
        fishCurrent++;
        
        if(fishCurrent >= fishGoal)
        {
            fishCurrent = fishGoal;
            started = false;
            Minigame_UI_Script.Instance.ShowVictoryPopup();
            if (saveSO.saveData.TryGetValue("Hawk", out bool value))
            {
                saveSO.saveData["Hawk"] = true;
            }
        }
        scoreLabel.text = $"Score {fishCurrent} / {fishGoal}";
    }
}