using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightAim : MonoBehaviour
{
    [SerializeField] private Transform flashlight;

    private void Update()
    {
        if (Mouse.current == null) return;

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - flashlight.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        flashlight.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}