using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentreArea : MonoBehaviour
{
    public static event Action<bool> OnPlayerInCentreArea;
    public static event Action OnFinalMoment;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerInCentreArea?.Invoke(true);
            if(Keyboard.current.eKey.isPressed)
            {
                OnFinalMoment?.Invoke();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerInCentreArea?.Invoke(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerInCentreArea?.Invoke(true);
        }
    }
}
