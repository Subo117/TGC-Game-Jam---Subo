using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject gameIntroScene;
    [SerializeField] TMP_Text introText;

    public void PlayGame()
    {
        StartCoroutine(PlayGameCoroutine());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator PlayGameCoroutine()
    {
        gameIntroScene.SetActive(true);

        introText.text = "Alan woke up and found himself surrounded by darkness...";
        yield return new WaitForSeconds(3f);

        introText.text = "Cakes were attacking him";
        yield return new WaitForSeconds(3f);

        introText.text = "Defeat them using your flash light and collect orbs";
        yield return new WaitForSeconds(3f);

        introText.text = "And use the orbs to lighten the centre and free the cakes";
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Game");
    }
}