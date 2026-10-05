using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Attack")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Orb")]
    [SerializeField, Range(0f, 100f)] private float orbSpawnChance = 50f;
    [SerializeField] private GameObject orbPrefab;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    private float currentHealth;
    private float nextAttackTime;

    private Transform player;

    private bool canChase = true;

    private void OnEnable()
    {
        CentreArea.OnFinalMoment += StopChasing;
        FinalMoment.OnStartJumping += StartJumping;
    }

    private void OnDisable()
    {
        CentreArea.OnFinalMoment -= StopChasing;
        FinalMoment.OnStartJumping -= StartJumping;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        if (CentreArea.FinalMomentStarted)
        {
            canChase = false;
        }
    }

    private void Update()
    {
        if (!canChase)
            return;

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

    private void FollowPlayer()
    {
        Vector2 direction = player.position - transform.position;

        transform.position += (Vector3)direction.normalized * moveSpeed * Time.deltaTime;
    }

    private void StopChasing()
    {
        canChase = false;
    }

    void StartJumping()
    {
        float randomDelay = Random.Range(0f, 1.5f);

        LeanTween.delayedCall(gameObject, randomDelay, () =>
        {
            LeanTween.moveY(gameObject, transform.position.y + 0.5f, 0.3f).setEaseOutQuad().setLoopPingPong();
        });
    }

    private void AttackPlayer()
    {
        if (Time.time < nextAttackTime)
            return;

        Player playerScript = player.GetComponent<Player>();

        if (playerScript != null)
        {
            playerScript.TakeDamage(attackDamage);
        }

        nextAttackTime = Time.time + attackCooldown;
    }

    public void TakeDamage(float damage)
    {
        if (!canChase)
            return;

        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        float randomChance = Random.Range(0f, 100f);

        if (randomChance <= orbSpawnChance)
        {
            if (orbPrefab != null)
            {
                Instantiate(orbPrefab, transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}