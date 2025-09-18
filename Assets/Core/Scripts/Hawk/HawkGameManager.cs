using System.Collections.Generic;
using UnityEngine;

public class HawkGameManager : MonoBehaviour
{
    [SerializeField] float timeBetweenFish = 5;
    float timer = 0;
    [SerializeField] GameObject fishPrefab;
    
    
    void Start()
    {
        Instantiate(fishPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenFish)
        {
            Instantiate(fishPrefab);
            timer = 0;
        }
    }
}
