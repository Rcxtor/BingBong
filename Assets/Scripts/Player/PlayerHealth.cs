using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int health = 10;

    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    public static Action<int> OnPlayerChangeHealth;
    public static Action<int> OnPlayerRestoreHealth;
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
        OnPlayerChangeHealth?.Invoke(currentHealth);
        animator.SetTrigger(flashRedAnim);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void RestoreHealth(int healthRestored) 
    { 
        currentHealth += healthRestored;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnPlayerChangeHealth?.Invoke(currentHealth);

    }
    private void OnEnable()
    {
        Fruit.OnFruitCollected += RestoreHealth;
    }
    private void OnDisable()
    {
        Fruit.OnFruitCollected -= RestoreHealth;
    }
}