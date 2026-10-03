using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightAim : MonoBehaviour
{
    public Transform flashlight;

    void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - flashlight.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        flashlight.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
