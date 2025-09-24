using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class QuizUI : MonoBehaviour
{
    // Fields
    [SerializeField]
    private UIDocument uiDocument;

    private Label questionLabel;
    private VisualElement buttonContainer;
    private UnityEngine.UIElements.Button buttonTemplate;

    private Label scoreLabel;
    private int score = 0;

    private bool started = false;
    private float startTimer = 0;

    // Results
    private VisualElement resultsVisualElement;
    private Label resultsLabel;
    private Button homeButton;

    //Cache for TemplateClasses 
    private List<string> templateClasses;

    private int currentQuestionIndex = 0;

    [Header("Save Settings")]
    [SerializeField] private SaveSO saveSO;

    private void Start()
    {
        currentQuestionIndex = 0;
        var root = uiDocument.rootVisualElement;

        questionLabel = root.Q<Label>("questionLabel");
        buttonContainer = root.Q<VisualElement>("buttonContainer");
        buttonTemplate = root.Q<UnityEngine.UIElements.Button>("buttonTemplate");

        resultsVisualElement = root.Q<VisualElement>("Results");
        resultsLabel = root.Q<Label>("ResultsLabel");
        homeButton = root.Q<Button>("HomeButton");
        homeButton.clicked += GoHome;

        // Cache TemplateClasses for answerButton styling
        templateClasses = buttonTemplate != null ? buttonTemplate.GetClasses().ToList() : null;

        if (buttonTemplate != null)
            buttonTemplate.style.display = DisplayStyle.None; // Hide template button

        scoreLabel = root.Q<Label>("scoreLabel");
    }
    private void Update()
    {
        if (startTimer < 0.5f)
        {
            startTimer += Time.deltaTime;
        }
        else if (!started)
        {
            started = true;
            StartQuiz();
        }
    }

    private void ShowQuestion()
    {
        var animal = GameManager.Instance.selectedAnimal;
        if (animal == null || animal.questions.Length == 0) return;

        // Get current question
        var question = animal.questions[currentQuestionIndex];
        questionLabel.text = question.questionText;

        // Clear old buttons
        buttonContainer.Clear();

        // Create buttons for answers
        for (int i = 0; i < question.answer.Length; i++)
        {
            var answerButton = new UnityEngine.UIElements.Button();
            answerButton.text = question.answer[i];
            
            //Add styling from buttonTemplate + null check just in case buttonTemplate does not exist in UXML document
            if (templateClasses != null)
                foreach (var cls in templateClasses)
                    answerButton.AddToClassList(cls);
                


            int index = i; //Capture index for the closure
            answerButton.clicked += () => OnAnswerSelected(index);


            answerButton.style.display = DisplayStyle.Flex;

            buttonContainer.Add(answerButton);
        }
    }

    private void OnAnswerSelected(int index)
    {
        
        var animal = GameManager.Instance.selectedAnimal;
        var question = animal.questions[currentQuestionIndex];

        if (index == question.correctAnswerIndex)
        {
            Debug.Log("Correct!");
            score++;
            UpdateScoreUI();
        }
            
        else
            Debug.Log("Wrong!");

        // Move to next question
        currentQuestionIndex++;
        if (currentQuestionIndex < animal.questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            Debug.Log("Quiz Finished!");
            resultsVisualElement.style.display = DisplayStyle.Flex;
            if (score < (animal.questions.Length / 2)+1)
            {
                resultsLabel.text = "Du fik ikke nok point, prøv igen for at få et klistermærke";
                
            }
            else
            {
                resultsLabel.text = "Tilykke, du fik nok point til at få et klistermærke";
                if (saveSO.saveData.TryGetValue(animal.name, out bool value))
                {
                    saveSO.saveData[animal.name] = true;
                }
            }

        }
    }

    private void UpdateScoreUI()
    {
        if (scoreLabel != null)
            scoreLabel.text = $"Score: {score}";
    }
    private void GoHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    private void StartQuiz()
    {
        ShowQuestion();
        UpdateScoreUI();
    }
}
