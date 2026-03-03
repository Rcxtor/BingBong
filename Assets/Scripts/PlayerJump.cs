using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float JumpForce = 7f;
    [SerializeField] private float DoubleJumpForce = 5f;
    [SerializeField] private Vector2 WallJumpForce = new Vector2(4f, 8f);
    [SerializeField] private float WallJumpCD = 0.1f;
    private PlayerMovement playerMovement;


    private float playerhalfHeight;
    private float playerhalfWidth;
    private float footOffset = 0.3f;
    private bool canDoubleJump;

    private void Start()
    {
        playerhalfHeight = spriteRenderer.bounds.extents.y;
        playerhalfWidth = spriteRenderer.bounds.extents.x;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            CheckJumpType();
        }


    }

    private void CheckJumpType()
    {
        if (isGrounded())
        {
            playerMovementState.setMoveState(PlayerMovementState.MoveState.jump);
            Jump(JumpForce);
            canDoubleJump = true;
        }
        else
        {
            int direction = GetWallJumpDirection();
            if (direction == 0 && canDoubleJump ) // for duble && rigid2D.linearVelocityY<=1f
            {
                DoubleJump(DoubleJumpForce);
            }
            else if(direction !=0)
            {
                WallJump(direction);
            }
        }
        
    }

    

    private bool isGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - footOffset, transform.position.y), Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + footOffset, transform.position.y), Vector2.down, playerhalfHeight + 0.1f, LayerMask.GetMask("Ground"));
        return leftHit.collider != null || rightHit.collider != null;
    }

    private void Jump(float force)
    {
        rigid2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void DoubleJump(float force)
    {
        rigid2D.linearVelocity = Vector2.zero;
        rigid2D.angularVelocity = 0;
        Jump(force);
        canDoubleJump = false;
        playerMovementState.setMoveState(PlayerMovementState.MoveState.double_jump);

    }
    private void WallJump(int direction)
    {
        Vector2 force = WallJumpForce;
        force.x *= direction;
        rigid2D.linearVelocity = Vector2.zero;
        rigid2D.angularVelocity = 0;
        playerMovement.WallJumpCD = WallJumpCD;
        rigid2D.AddForce(force, ForceMode2D.Impulse);
        playerMovementState.setMoveState(PlayerMovementState.MoveState.wall_jump);

    }

    private int GetWallJumpDirection()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, playerhalfWidth + 0.1f, LayerMask.GetMask("Wall")))
        {
            return -1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, playerhalfWidth + 0.1f, LayerMask.GetMask("Wall")))
        {
            return 1;
        }

        return 0;
    }
}
