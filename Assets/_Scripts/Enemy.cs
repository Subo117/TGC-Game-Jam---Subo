using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1f;

    [SerializeField, Range(0f, 100f)] private float orbSpawnChance = 50f;
    [SerializeField] private GameObject orbPrefab;

    [SerializeField] private Slider healthBar;
    private float currentHealth;

    private Transform player;
    private float nextAttackTime;

    private bool canChase = true;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        CentreArea.OnFinalMoment += StopChasing;
    }

    private void OnDisable()
    {
        CentreArea.OnFinalMoment -= StopChasing;
    }

    void Update()
    {
        if (!canChase) return;

        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            FollowPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = player.position - transform.position;

        transform.position += (Vector3)direction.normalized * moveSpeed * Time.deltaTime;
    }

    void StopChasing()
    {
        canChase = false;
    }
    void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            Player playerScript = player.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(attackDamage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void TakeDamage(float damage)
    {
        if(!canChase) return;

        currentHealth -= damage;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        float randomChance = Random.Range(0f, 100f);

        if (randomChance <= orbSpawnChance)
        {
            Instantiate(orbPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
