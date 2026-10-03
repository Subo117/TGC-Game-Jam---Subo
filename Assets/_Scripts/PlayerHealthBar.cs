using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    void Start()
    {
        healthSlider.maxValue = player.MaxHealth;
        healthSlider.value = player.MaxHealth;
    }

    void Update()
    {
        healthSlider.value = player.CurrentHealth;
    }
}
