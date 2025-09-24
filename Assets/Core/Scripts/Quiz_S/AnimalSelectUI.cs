using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class AnimalSelect : MonoBehaviour
{
    //Fields
    [SerializeField]
    private UIDocument UIDocument;
    public AnimalData[] availableAnimals; //Drag animaldata assets into the inspector
    public string quizSceneName = "QuizScene"; //Name of the quiz scene

    private VisualElement buttonContainer;

    private void OnEnable()
    {
        var root = UIDocument.rootVisualElement;
        buttonContainer = root.Q<VisualElement>("animalButtonContainer");
       // Debug.Log("Found container: " + buttonContainer);

        buttonContainer.Clear();

        foreach (var animal in availableAnimals)
        {
            var btn = new Button();
            btn.text = animal.animalName;

            btn.clicked += () =>
            {
                GameManager.Instance.SetAnimal(animal);
                SceneManager.LoadScene(quizSceneName);
            };

            buttonContainer.Add(btn);
        }
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
