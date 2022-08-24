using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float currentHealth;
    public Slider statusBar;

    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private Image fill;

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
        {
            Application.Quit();
        }
    }
    public void ChangeFillColor(Color color)
    {
        fill.color = color;
    }
}
