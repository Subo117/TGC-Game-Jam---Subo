using System;
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
        player.OnHealthChanged += UpdateHealthBar;
        CentreArea.OnPlayerInCentreArea += OnPlayerInCentreArea;

    }


    private void OnDisable()
    {
        player.OnHealthChanged -= UpdateHealthBar;
        CentreArea.OnPlayerInCentreArea -= OnPlayerInCentreArea;

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

        if(ScoreManager.Instance.CurrentOrb >= maxOrb)
        {
            instructionText.text = "Rush toward the centre area!!";
            OnMaxOrbReached?.Invoke();
        }
    }

    private void OnPlayerInCentreArea(bool isInArea)
    {
        if(isInArea) instructionText.text = "Press E to Upgrade";
        else instructionText.text = "Rush toward the centre area!!";
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
