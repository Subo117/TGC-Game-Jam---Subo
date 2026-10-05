using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    [SerializeField] private int value = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        //AudioManager.Instance.PlayXPClip();

        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(value);
            }

            Destroy(gameObject);
        }
    }
}