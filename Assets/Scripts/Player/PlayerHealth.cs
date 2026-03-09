using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int health = 10;

    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    public static Action<int> OnPlayerTakeDamage;
    public static Action OnPlayerDie;

    private const string flashRedAnim = "FlashRed";

    private void Awake()
    {
        currentHealth = health;
        maxHealth = health;

        //Debug.Log("Current Health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnPlayerTakeDamage?.Invoke(currentHealth);
        animator.SetTrigger(flashRedAnim);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}