using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private float playerhalfHeight;

    private void Start()
    {
        playerhalfHeight = spriteRenderer.bounds.extents.y;
    }
    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down* (playerhalfHeight + 0.1f), Color.red);
        if(Input.GetButtonDown("Jump") && isGrounded())
        {

            jump();
        }

    }
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }
    private void jump()
    {
        rigid2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}
