using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Fields
    public static GameManager Instance;
    public AnimalData selectedAnimal;
    public QuestionData currentQuestion;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetAnimal(AnimalData animal)
    {
        selectedAnimal = animal;
        PickRandomQuestion();
    }

    private void PickRandomQuestion()
    {
        if (selectedAnimal.questions.Length == 0) return;
        int randomIndex = Random.Range(0, selectedAnimal.questions.Length);
        currentQuestion = selectedAnimal.questions[randomIndex];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
