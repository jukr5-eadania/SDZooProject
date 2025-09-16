using UnityEngine;
using UnityEngine.InputSystem;

public class BearInputHandler : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Click(Mouse.current.position.ReadValue());
        }
    }

    private void Click(Vector2 screenPos)
    {
        Vector2 worldPos = mainCam.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            BearFinder bearFinder = hit.collider.GetComponent<BearFinder>();

            if (bearFinder != null)
            {
                bearFinder.OnFound();
            }
        } 
    }
}
