using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private StartSide ss;
    
    [SerializeField] private float moveAmount = 1;

    [Header("Fish Sprites")]
    [SerializeField] private List<Sprite> sprites = new();

    private enum StartSide
    {
        Left,
        Right
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = sprites[Random.Range(0,sprites.Count)];

        float xPos;
        float yPos = Camera.main.ScreenToWorldPoint(new Vector3(
            0, 
            Camera.main.pixelHeight * Random.Range(0.3f, 0.7f), 
            0)).y;

        ss = (StartSide)Random.Range(0, 2);

        if (ss == StartSide.Left)
        {
            xPos = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
        }
        else
        {
            xPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0)).x + 1;
        }
        gameObject.transform.position = new Vector3(xPos, yPos);

        float angle = 0;
        if (ss == StartSide.Left)
        {
            angle = Random.Range(25, 65);
        }
        else
        {
            angle = Random.Range(115, 155);
        }
        float radians = angle * Mathf.Deg2Rad;

        rb.AddForce(new Vector2(
            Mathf.Cos(radians), 
            Mathf.Sin(radians)) * 
            moveAmount);


        
    }

    void Update()
    {
        Vector2 moveDirection = transform.right;
        transform.right = rb.linearVelocity;
        if (rb.linearVelocityX < 0)
        {
            sr.flipY = true;
        }
        else
        {
            sr.flipY = false;
        }

        if (gameObject.transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y)
        {
            Destroy(gameObject);
        }
    }
}
