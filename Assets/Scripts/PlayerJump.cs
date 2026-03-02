using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private float playerhalfHeight;
    private float footOffset = 0.3f;

    private void Start()
    {
        playerhalfHeight = spriteRenderer.bounds.extents.y;
    }
    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x - footOffset, transform.position.y), Vector2.down* (playerhalfHeight + 0.1f), Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + footOffset, transform.position.y), Vector2.down* (playerhalfHeight + 0.1f), Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded())
        {

            jump();
        }

    }
    private bool isGrounded()
    {
        //return Physics2D.Raycast(transform.position, Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - footOffset, transform.position.y), Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + footOffset, transform.position.y), Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        return leftHit.collider != null || rightHit.collider != null;
    }

    private void jump()
    {
        rigid2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}
