using UnityEngine;

public class PolarBearLogic : MonoBehaviour
{
    public static PolarBearLogic Instance { get; private set; }
    private float timer = 0;
    private bool eating = false;
    private Vector2 startPos;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite attackSprite;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        startPos = gameObject.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eating)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                gameObject.transform.position = startPos;
                timer = 0;
                eating = false;
                spriteRenderer.sprite = idleSprite;
            }
        }
    }

    public void Eat(GameObject target)
    {
        Vector2 targetPos = target.transform.position; ;
        gameObject.transform.position = new Vector2(targetPos.x + 1.5f,targetPos.y);
        eating = true;
        spriteRenderer.sprite = attackSprite;
    }
}
