using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Script Reference")]
    [SerializeField] private Player player;

    [Header("UI Elements")]
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider darkOrbBar;

    [Header("Values")]
    [SerializeField] private int maxOrb = 500;


    private void OnEnable()
    {
        player.OnHealthChanged += UpdateHealthBar;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateOrbBar;
        }
    }

    private void OnDisable()
    {
        player.OnHealthChanged -= UpdateHealthBar;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateOrbBar;
        }
    }

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateOrbBar;

        playerHealthBar.maxValue = player.MaxHealth;
        playerHealthBar.value = player.CurrentHealth;

        darkOrbBar.minValue = 0;
        darkOrbBar.maxValue = maxOrb;
        darkOrbBar.value = ScoreManager.Instance.CurrentOrb;
    }

    private void UpdateHealthBar()
    {
        playerHealthBar.value = player.CurrentHealth;
    }

    private void UpdateOrbBar(int score)
    {
        Debug.Log("In UpdateOrbBar");
        darkOrbBar.value = score;
    }
}
