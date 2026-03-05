using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1f;
    private int startDirection = 1;
    private int currentDirection;
    private float halfWidth;
    private Vector2 movement;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false: true; // 1 = going right
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.x = speed * currentDirection;
        movement.y = rb.linearVelocityY;
        rb.linearVelocity = movement;
        setDirection();
    }
    private void setDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rb.linearVelocityX > 0)
        {
            currentDirection *= -1;
            spriteRenderer.flipX = true;
        }
        else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rb.linearVelocityX < 0)
        {
            currentDirection = 1; // or *= -1
            spriteRenderer.flipX = false;

        }
    }          
}
