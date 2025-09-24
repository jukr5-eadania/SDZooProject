using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DragHawk : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private enum State
    {
        Idle, Dragging, Flying, Returning
    }
    [SerializeField] private State state = State.Idle;
    [SerializeField] private float moveAmount = 1;

    [SerializeField] MonoBehaviour GameManager;

    private SpriteRenderer rend;
    private Vector3 offset;
    private float startHeight;


    void Start()
    {
        startHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight * 0.1f, 0)).y;
        rend = GetComponent<SpriteRenderer>();
        Bounds();
    }
    
    void Update()
    {
        if (state == State.Dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Touchscreen.current.position.x.value, Touchscreen.current.position.y.value)) + offset;
            Bounds();
        }

        if (state == State.Flying || state == State.Returning)
        {
            Fly();
        }

        if (state == State.Returning && transform.position.y >= startHeight)
        {
            state = State.Idle;
            Bounds();
        }
    }
    private void Bounds()
    {
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);

        if (transform.position.x - rend.bounds.extents.x < Camera.main.ScreenToWorldPoint(Vector3.zero).x)
        {
            transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Vector3.zero).x + rend.bounds.extents.x, 
                transform.position.y, 
                transform.position.z);
        }
        if (transform.position.x + rend.bounds.extents.x > Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth,0,0)).x)
        {
            transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)).x - rend.bounds.extents.x, 
                transform.position.y, 
                transform.position.z);
        }
    }

    private void Fly()
    {
        transform.position += Vector3.up * Time.deltaTime * moveAmount;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (state == State.Idle || state == State.Dragging)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Touchscreen.current.position.x.value, Touchscreen.current.position.y.value));
            state = State.Dragging;
        }
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        state = State.Flying;
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y > startHeight)
        {
            state = State.Returning;
            transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 0)).x,
                Camera.main.ScreenToWorldPoint(Vector3.zero).y - rend.bounds.extents.y,
                transform.position.z); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            Destroy(collision.gameObject);

            GameManager.SendMessage("IncreaseScore", SendMessageOptions.DontRequireReceiver);
        }
    }
}