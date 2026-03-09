using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    

    public float WallJumpCD { get; set; }
    private Vector2 knockBackToPlayer = new Vector2(6f, 4f);
    private Vector2 movement;
    private float xPosLast;
    private Vector2 particleStartPos;

    private void Start()
    {
        particleStartPos = dustParticle.transform.localPosition;
    }
    void Update()
    {
        handelMovement();
        StSpSparticle();
        flipCharX();
        if (WallJumpCD > 0f)
        {
            WallJumpCD -= Time.deltaTime;
        }
        xPosLast = transform.position.x;

    }

    private void flipCharX()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input > 0 && transform.position.x > xPosLast)
        {
            spriteRenderer.flipX = false; //right movement
            dustParticle.transform.localPosition = particleStartPos;
            dustParticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (input < 0 && transform.position.x < xPosLast)
        {
            spriteRenderer.flipX = true;
            Vector2 particlePos = particleStartPos;
            particleStartPos.x *= -1f;
            dustParticle.transform.localPosition = particleStartPos;
            dustParticle.transform.rotation = Quaternion.Euler(0, 180, 0);

        }

        //xPosLast = transform.position.x;
    }
    private void StSpSparticle()
    {
        if(rb.linearVelocityY !=0 || xPosLast == transform.position.x)
        {
            dustParticle.Stop();
        }
        else
        {
            if(!dustParticle.isPlaying)
            {
                dustParticle.Play();
            }
        }
    }
    public void KnockBackPlayer(int direction)
    {
        //print("KNOCKBACK");
        //WallJumpCD = 0.2f;
        knockBackToPlayer.x *= direction;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(knockBackToPlayer, ForceMode2D.Impulse);
        //WallJumpCD = 0f;
    }
    private void handelMovement()
    {
        if (WallJumpCD > 0f) return;

        float input = Input.GetAxisRaw("Horizontal");
        movement.x = input * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
