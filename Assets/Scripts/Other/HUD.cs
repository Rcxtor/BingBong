
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI counterText;
    private int maxHealth;
    private int fruitCollectedAmmount;

    private void SetupHealthBar(GameObject player)
    {
        healthBar.value = healthBar.maxValue;
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
    }
    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = (float)currentHealth / maxHealth;
        healthBar.value = Mathf.Clamp01(healthBar.value);
    }
    private void OnEnable()
    {
        GameController.OnPlayerSpawned += SetupHealthBar;
        PlayerHealth.OnPlayerChangeHealth += UpdateHealthBar;
        Fruit.OnFruitCollected += CollectedFruit;
    }
    private void CollectedFruit(int healthRestored)
    {
        fruitCollectedAmmount++;
        counterText.text = $"x{fruitCollectedAmmount}";
    }

    private void OnDisable()
    {
        GameController.OnPlayerSpawned -= SetupHealthBar;
        PlayerHealth.OnPlayerChangeHealth -= UpdateHealthBar;
        Fruit.OnFruitCollected -= CollectedFruit;


    }
}
