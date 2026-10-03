using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    [SerializeField] int value = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                ScoreManager.Instance.AddScore(value);
                Destroy(gameObject);
            }
        }
    }
}
