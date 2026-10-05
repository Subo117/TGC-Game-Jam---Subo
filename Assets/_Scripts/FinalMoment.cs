using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Unity.Cinemachine;

public class FinalMoment : MonoBehaviour
{
    [Header("Global Environment")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float transitionTime = 10f;
    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor = Color.white;

    [Header("Player Flash Light")]
    [SerializeField] private Light2D playerFlashLight;
    [SerializeField] private float startIntensity = 4f;
    [SerializeField] private float targetIntensity = 0f;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private float startLens = 13f;
    [SerializeField] private float targetLens = 13f;

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

    IEnumerator DoFinalMomentRoutine()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(ChangeColor());
        StartCoroutine(ChangeIntensity());
        StartCoroutine(ChangeLens());
    }

    private IEnumerator ChangeColor()
    {
        float timer = 0f;
        startColor = globalLight.color;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;
            globalLight.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        globalLight.color = targetColor;
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

    private IEnumerator ChangeLens()
    {
        float timer = 0f;
        startLens = cam.Lens.OrthographicSize;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;
            cam.Lens.OrthographicSize = Mathf.Lerp(startLens, targetLens, t);
            yield return null;
        }

        cam.Lens.OrthographicSize = targetLens;
    }

}