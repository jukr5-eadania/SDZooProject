using UnityEngine;
using UnityEngine.InputSystem;

public class HR_wolf : MonoBehaviour
{
    private HR_GameManager gm;
    private SpriteRenderer sr;
    private HR_Input input;
    private AudioSource audioSource;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite howlingSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip howlClip;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
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
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (GetComponent<Collider2D>().OverlapPoint(tapPos))
        {
            StartCoroutine(DoHowl(1.0f));
            gm.RegisterWolfTap(this);
        }
    }

    public System.Collections.IEnumerator DoHowl(float duration)
    {
        sr.sprite = howlingSprite;
        audioSource.PlayOneShot(howlClip);
        yield return new WaitForSeconds(duration);
        sr.sprite = idleSprite;
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
