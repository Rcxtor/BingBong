using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockBackToPlayer=new Vector2 (8f,4f);
    
    public void Die()
    {
        Destroy(gameObject);
        // need to add animation
    }
    public void hitPlayer(Transform playerTransform)
    {
        print("player hit");
        int direction = getDirection(playerTransform);
        FindAnyObjectByType<PlayerMovement>().KnockBackPlayer(knockBackToPlayer,direction);
    }

    private int getDirection(Transform playerTransform)
    {
        if(transform.position.x > playerTransform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
