using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Unity.Cinemachine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalMoment : MonoBehaviour
{
    [Header("Global Environment")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float transitionTime = 10f;
    [SerializeField] private Color startColor;
    [SerializeField] private float globalStartIntensity = 1f;
    [SerializeField] private float globalTargetIntensity = 2f;
    [SerializeField] private Color targetColor = Color.white;

    [Header("Player Flash Light")]
    [SerializeField] private Light2D playerFlashLight;
    [SerializeField] private float startIntensity = 4f;
    [SerializeField] private float targetIntensity = 0f;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private float startLens = 13f;
    [SerializeField] private float targetLens = 13f;

    [Header("Enemy Sprites")]
    [SerializeField] private Sprite finalEnemySprite;

    [Header("Disco Light")]
    [SerializeField] private float discoChangeTime = 0.3f;
    [SerializeField] private Color[] discoColors;

    

    private int currentColorIndex = 0;

    public static event Action OnStartJumping;

    private void OnEnable()
    {
        CentreArea.OnFinalMoment += DoFinalMoment;
    }

    private void OnDisable()
    {
        CentreArea.OnFinalMoment -= DoFinalMoment;
    }

    private void DoFinalMoment()
    {
        StartCoroutine(DoFinalMomentRoutine());
    }

    private IEnumerator ChangeEnemySprites()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();


            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = finalEnemySprite;
            }

            Canvas canvas = enemy.GetComponentInChildren<Canvas>();

            if (canvas != null)
            {
                Destroy(canvas.gameObject);
            }

        }
        yield return new WaitForSeconds(1f);

        OnStartJumping?.Invoke();

        yield return new WaitForSeconds(1f);

        StartCoroutine(DiscoLight());

        AudioManager.Instance.PlayDiscoMusic();

        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene("Game End");
    }

    IEnumerator DoFinalMomentRoutine()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(ChangeColor());
        StartCoroutine(ChangeIntensity());

        yield return new WaitForSeconds(2f);

        StartCoroutine(ChangeLens(transitionTime / 2f));

        yield return new WaitForSeconds(3f);

        StartCoroutine(ChangeEnemySprites());
    }

    private IEnumerator ChangeColor()
    {
        float timer = 0f;

        startColor = globalLight.color;
        globalStartIntensity = globalLight.intensity;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;
            globalLight.color = Color.Lerp(startColor, targetColor, t);
            globalLight.intensity = Mathf.Lerp(globalStartIntensity, globalTargetIntensity, t);
            yield return null;
        }

        globalLight.color = targetColor;
        globalLight.intensity = globalTargetIntensity;
    }

    private IEnumerator ChangeIntensity()
    {
        float timer = 0f;
        startIntensity = playerFlashLight.intensity;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;
            playerFlashLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        playerFlashLight.intensity = targetIntensity;
    }

    private IEnumerator ChangeLens(float time)
    {
        float timer = 0f;
        startLens = cam.Lens.OrthographicSize;

        while (timer < time)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            cam.Lens.OrthographicSize = Mathf.Lerp(startLens, targetLens, t);
            yield return null;
        }

        cam.Lens.OrthographicSize = targetLens;
    }

    private IEnumerator DiscoLight()
    {
        while (true)
        {
            globalLight.color = discoColors[currentColorIndex];

            currentColorIndex++;

            if (currentColorIndex >= discoColors.Length)
            {
                currentColorIndex = 0;
            }

            yield return new WaitForSeconds(discoChangeTime);
        }
    }

    

}