using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int healthRecovered = 2;

    public static Action<int> OnFruitCollected;

    public void Collect()
    {
        animator.SetTrigger("Collect");
        OnFruitCollected.Invoke(healthRecovered);
    }
}
