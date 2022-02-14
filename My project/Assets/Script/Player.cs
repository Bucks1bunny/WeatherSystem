using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Health
    [Header("Health system")]
    public HealthBar healthBar;
    public float maxHealth = 100;
    public static float currentHealth;

    [Header("Status bar")]
    public Slider statusBar;
    public Image fill;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
             Debug.Log("Dead");
    }
    public void ChangeFillColor(Color color)
    {
        fill.color = color;
    }
}
