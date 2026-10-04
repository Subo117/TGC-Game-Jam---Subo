using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Script Reference")]
    [SerializeField] private Player player;

    [Header("Slider")]
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider darkOrbBar;

    [Header("Screen")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Values")]
    [SerializeField] private int maxOrb = 20;


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

        playerHealthBar.interactable = false;
        darkOrbBar.interactable = false;
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
