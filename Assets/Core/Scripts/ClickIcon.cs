using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int sceneIndex;
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}