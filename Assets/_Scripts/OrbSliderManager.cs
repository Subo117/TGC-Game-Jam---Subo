using UnityEngine;
using UnityEngine.UI;

public class OrbSliderManager : MonoBehaviour
{
    [SerializeField] private Slider orbSlider;

    [SerializeField] private int maxOrb = 500;

    private void Start()
    {
        orbSlider.maxValue = maxOrb;
        orbSlider.minValue = 0;
        orbSlider.value = 0;
    }

    private void Update()
    {
        orbSlider.value = ScoreManager.Instance.CurrentOrb;
    }

}
