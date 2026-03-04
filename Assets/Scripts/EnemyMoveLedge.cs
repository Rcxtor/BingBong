using UnityEngine;

public class EnemyMovementLedge : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1f;
    private int startDirection = 1;
    private int currentDirection;
    private float halfheight;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfheight = spriteRenderer.bounds.extents.y;
        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true; // 1 = going right
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
        if (rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = false;

            if (!Physics2D.Raycast(new Vector2(transform.position.x + 0.2f, transform.position.y), Vector2.down, halfheight + 0.1f, LayerMask.GetMask("Ground")))
            {   
                currentDirection *= -1;
            }
        }
        else if(rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = true;

            if (!Physics2D.Raycast(new Vector2(transform.position.x - 0.2f, transform.position.y), Vector2.down, halfheight + 0.1f, LayerMask.GetMask("Ground")))
                currentDirection = 1; // or *= -1
        }
    }
}
