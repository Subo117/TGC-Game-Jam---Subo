using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentreArea : MonoBehaviour
{
    public static event Action<bool> OnPlayerInCentreArea;
    public static event Action OnFinalMoment;

    private bool isMAxOrbReached = false;
    private bool playerInCentre = false;

    private void OnEnable()
    {
        UIManager.OnMaxOrbReached += OnMaxOrbReached;
    }

    private void OnDisable()
    {
        UIManager.OnMaxOrbReached -= OnMaxOrbReached;
    }

    private void OnMaxOrbReached()
    {
        isMAxOrbReached = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMAxOrbReached)
        {
            Debug.Log("Player Entered Centre Area");
            playerInCentre = true;
            OnPlayerInCentreArea?.Invoke(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMAxOrbReached)
        {
            playerInCentre = false;
            OnPlayerInCentreArea?.Invoke(false);
        }
    }

    private void Update()
    {
        if (playerInCentre && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Final Moment Triggered");
            OnFinalMoment?.Invoke();
        }
    }
}
