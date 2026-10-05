using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform healthUI;
    [SerializeField] private RectTransform orbUI;

    [SerializeField] private float duration = 0.8f;

    private Vector2 healthTarget;
    private Vector2 orbTarget;

    private void OnEnable()
    {
        UIManager.OnMaxOrbReached += RemoveUI;
    }
    private void OnDisable()
    {
        UIManager.OnMaxOrbReached -= RemoveUI;
    }
    private void Start()
    {
        healthTarget = healthUI.anchoredPosition;
        orbTarget = orbUI.anchoredPosition;

        healthUI.anchoredPosition = new Vector2(-Screen.width, healthTarget.y);
        orbUI.anchoredPosition = new Vector2(orbTarget.x, -Screen.height);

        LeanTween.move(healthUI, healthTarget, duration).setEase(LeanTweenType.easeOutBack);

        LeanTween.move(orbUI, orbTarget, duration).setEase(LeanTweenType.easeOutBack);
    }

    void RemoveUI()
    {
        LeanTween.move(healthUI, new Vector2(-Screen.width, healthTarget.y), duration).setEase(LeanTweenType.easeInBack);
        LeanTween.move(orbUI, new Vector2(orbTarget.x, -Screen.height), duration).setEase(LeanTweenType.easeInBack);
    }
}
