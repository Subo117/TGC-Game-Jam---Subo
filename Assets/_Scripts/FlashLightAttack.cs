using UnityEngine;

public class FlashLightAttack : MonoBehaviour
{
    [SerializeField] private float damage = 25f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage * Time.deltaTime);
        }
    }
}