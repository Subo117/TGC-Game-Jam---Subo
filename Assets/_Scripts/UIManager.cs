using System;
using System.Collections;
using TMPro;
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

    [Header("Texts")]
    [SerializeField] private TMP_Text instructionText;

    [Header("Values")]
    [SerializeField] private int maxOrb = 500;

    public static event Action OnMaxOrbReached;

    private void OnEnable()
    {
        if (player != null)
        {
            player.OnHealthChanged += UpdateHealthBar;
        }

        CentreArea.OnPlayerInCentreArea += OnPlayerInCentreArea;
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnHealthChanged -= UpdateHealthBar;
        }

        CentreArea.OnPlayerInCentreArea -= OnPlayerInCentreArea;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateOrbBar;
        }
    }

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateOrbBar;
        }

        playerHealthBar.maxValue = player.MaxHealth;
        playerHealthBar.value = player.CurrentHealth;

        darkOrbBar.minValue = 0;
        darkOrbBar.maxValue = maxOrb;
        darkOrbBar.value = ScoreManager.Instance.CurrentOrb;

        playerHealthBar.interactable = false;
        darkOrbBar.interactable = false;

        StartCoroutine(StartText());
    }

    private void UpdateHealthBar()
    {
        playerHealthBar.value = player.CurrentHealth;
    }

    private void UpdateOrbBar(int score)
    {
        darkOrbBar.value = score;

        if (score >= maxOrb)
        {
            instructionText.text = "Rush toward the centre area!!";

            OnMaxOrbReached?.Invoke();
        }
    }

    private void OnPlayerInCentreArea(bool isInArea)
    {
        if (isInArea)
        {
            instructionText.text = "Press E to Upgrade";
        }
        else
        {
            instructionText.text = "Rush toward the centre area!!";
        }
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

    IEnumerator StartText()
    {
        instructionText.text = "Cakes are attacking me!!";
        yield return new WaitForSeconds(5f);
        instructionText.text = "I have to fight them";
        yield return new WaitForSeconds(5f);
        instructionText.text = "";

    }
}