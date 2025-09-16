using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class HR_wolf : MonoBehaviour
{
    private HR_GameManager gm;
    private SpriteRenderer sr;
    private HR_Input input;
    private AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] private HR_wolf_SO settings;
    [SerializeField] private float howlDuration = 1.0f;

    [Header("Events")]
    public UnityEvent onWolfHowl;
    public UnityEvent onWolfFail;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = settings.idleSprite;
        gm = HR_GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        input = new HR_Input();
        input.Player.Enable();
        input.Player.Howl.performed += OnHowl;
    }

    private void OnDisable()
    {
        input.Player.Howl.performed -= OnHowl;
        input.Player.Disable();
    }

    private void OnHowl(InputAction.CallbackContext context)
    {
        if (!gm.PlayerTurn) return;
        Debug.Log("OnTap called");

        if (Camera.main == null) Debug.LogError("Camera.main is NULL! Tag your camera as MainCamera.");
        if (Mouse.current == null) Debug.LogWarning("Mouse.current is NULL (expected on mobile).");
        if (Touchscreen.current == null) Debug.LogWarning("Touchscreen.current is NULL (expected in editor).");
        if (gm == null) Debug.LogError("GameManager reference is NULL!");

        Vector2 tapPos = Vector2.zero;

        if (Mouse.current != null)
        {
            tapPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        else if (Touchscreen.current != null)
        {
            tapPos = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        else
        {
            Debug.LogWarning("No Mouse or Touchscreen input found.");
            return;
        }

        if (GetComponent<Collider2D>().OverlapPoint(tapPos))
        {
            StartCoroutine(DoHowl());
            gm.RegisterWolfTap(this);
        }
    }

    public IEnumerator DoHowl()
    {
        sr.sprite = settings.howlingSprite;
        audioSource.PlayOneShot(settings.howlClip);
        onWolfHowl?.Invoke();
        yield return new WaitForSeconds(settings.howlDuration);
        sr.sprite = settings.idleSprite;
        yield return new WaitForSeconds(settings.howlDuration);
    }
}
