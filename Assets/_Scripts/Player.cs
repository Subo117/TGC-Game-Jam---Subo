using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackDamage = 25f;

    private float currentHealth;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    public event Action OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0f) currentHealth = 0f;

        OnHealthChanged?.Invoke();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Attack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");

        Time.timeScale = 0f;
    }
}