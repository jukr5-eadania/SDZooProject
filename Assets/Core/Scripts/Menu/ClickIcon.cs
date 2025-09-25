using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string sceneName;

    [SerializeField] private AnimalData animalData;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (animalData == null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            GameManager.Instance.selectedAnimal = animalData;
            SceneManager.LoadScene("QuizScene", LoadSceneMode.Single);
        }
    }
}