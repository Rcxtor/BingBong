using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator _animator;

    public float WallJumpCD {  get;set; }

    private Vector2 movement;
    private float xPosLast;

    // Update is called once per frame
    void Update()
    {
        handelMovement();
        flipCharX();
        if(WallJumpCD >0f)
        {
            WallJumpCD -= Time.deltaTime;
        }
    }

    private void flipCharX()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input>0 && transform.position.x > xPosLast)
        {
            spriteRenderer.flipX = false; //right movement
        }
        else if (input<0 && transform.position.x < xPosLast)
        {
            spriteRenderer.flipX = true;
        }

        xPosLast = transform.position.x;
    }
    private void handelMovement()
    {
        if (WallJumpCD > 0f) return;
        
        float input = Input.GetAxisRaw("Horizontal");
        movement.x = input * speed * Time.deltaTime;
        transform.Translate(movement);
        //if (input != 0)
        //{
        //    _animator.SetBool("isRunning", true);
        //}
        //else if (input == 0)
        //{
        //    _animator.SetBool("isRunning", false);
        //}
    }
}
