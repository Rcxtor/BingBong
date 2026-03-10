using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    
    public void hitPlayer(Transform playerTransform)
    {
        //print("player hit");
        int direction = getDirection(playerTransform);
        FindAnyObjectByType<PlayerMovement>().KnockBackPlayer(direction);
        FindAnyObjectByType<PlayerHealth>().TakeDamage(damage);
    }

    private int getDirection(Transform playerTransform)
    {
        if (transform.position.x > playerTransform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
