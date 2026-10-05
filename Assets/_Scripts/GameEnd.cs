using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [Header("Game End Screen")]
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private TMP_Text gameEndText;

    private void Start()
    {
        StartCoroutine(ShowGameEndScreen());
    }
    IEnumerator ShowGameEndScreen()
    {
        gameEndScreen.SetActive(true);

        gameEndText.text = "You have defeated the darkness and saved the cakes";
        yield return new WaitForSeconds(3f);

        gameEndText.text = "Alan Wake up and realises it was just his nightmare";
        yield return new WaitForSeconds(3f);

        gameEndText.text = "Yet another diabetic nightmare!!";
        yield return new WaitForSeconds(3f);

        gameEndText.text = "Anyways...";
        yield return new WaitForSeconds(3f);

        gameEndText.text = "Thank You for playing!";
        yield return new WaitForSeconds(3f);

        gameEndText.text = "Subo... Signing off!!";
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Main Menu");

        yield return null;
    }
}
