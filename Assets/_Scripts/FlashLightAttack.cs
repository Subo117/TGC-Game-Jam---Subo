using UnityEngine;

public class FlashLightAttack : MonoBehaviour
{
    public float damage = 25f;
    public float damageInterval = 0.5f;

    //private float nextDamageTime;

    void OnTriggerStay2D(Collider2D other)
    {
        //if (Time.time < nextDamageTime)
        //    return;

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage * Time.deltaTime);
            //nextDamageTime = Time.time + damageInterval;
        }
    }
}
