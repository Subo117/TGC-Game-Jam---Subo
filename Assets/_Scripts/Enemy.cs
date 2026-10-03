using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private float nextAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
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
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
