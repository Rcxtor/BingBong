using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float bounceForce =5f;
    [SerializeField] private Rigidbody2D rb;

    private float halfHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfHeight = spriteRenderer.bounds.extents.y;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
           other.GetComponent<Fruit>().Collect();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            CollisionWithEnemy(other);
        }
    }
    private void CollisionWithEnemy(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        ContactPoint2D contact = other.contacts[0];

        if (contact.normal.y > 0.5f) // The player is hitting the top side of the enemy
        {
            
            Vector2 velocity = rb.linearVelocity;
            velocity.y = 0f; 
            rb.linearVelocity = velocity;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            enemy.Die();
        }
        else
        {
    
            enemy.hitPlayer(transform);
        }
    }
}
