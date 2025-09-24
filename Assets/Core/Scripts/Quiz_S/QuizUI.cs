using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

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

    //Cache for TemplateClasses 
    private List<string> templateClasses;

    private int currentQuestionIndex = 0;

    private void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        questionLabel = root.Q<Label>("questionLabel");
        buttonContainer = root.Q<VisualElement>("buttonContainer");
        buttonTemplate = root.Q<UnityEngine.UIElements.Button>("buttonTemplate");

        // Cache TemplateClasses for answerButton styling
        templateClasses = buttonTemplate != null ? buttonTemplate.GetClasses().ToList() : null;

        if (buttonTemplate != null)
            buttonTemplate.style.display = DisplayStyle.None; // Hide template button

        ShowQuestion();

        scoreLabel = root.Q<Label>("scoreLabel");
        UpdateScoreUI();
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
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreLabel != null)
            scoreLabel.text = $"Score: {score}";
    }
}
