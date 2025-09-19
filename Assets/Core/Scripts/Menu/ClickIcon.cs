using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string sceneName;
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}