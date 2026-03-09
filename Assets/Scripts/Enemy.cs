using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
        // need to add animation like poof
    }
    public void hitPlayer(Transform playerTransform)
    {
        //print("player hit");
        int direction = getDirection(playerTransform);
        FindAnyObjectByType<PlayerMovement>().KnockBackPlayer(direction);
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
