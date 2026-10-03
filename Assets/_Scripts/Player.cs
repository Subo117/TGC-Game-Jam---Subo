using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackDamage = 25f;

    private float currentHealth;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(attackDamage);
    }


    void Die()
    {
        Debug.Log("Player Died");
        Time.timeScale = 0f; // Pause the game
    }
}
