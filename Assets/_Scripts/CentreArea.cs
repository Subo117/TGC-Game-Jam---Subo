using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CentreArea : MonoBehaviour
{
    public static event Action<bool> OnPlayerInCentreArea;
    public static event Action<String> ChangeInstructionText;
    public static event Action OnFinalMoment;

    public static bool FinalMomentStarted { get; private set; }

    private bool isMaxOrbReached = false;
    private bool playerInCentre = false;

    private void Awake()
    {
        FinalMomentStarted = false;
    }

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
        isMaxOrbReached = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMaxOrbReached)
        {
            Debug.Log("Player Entered Centre Area");

            playerInCentre = true;

            OnPlayerInCentreArea?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMaxOrbReached)
        {
            playerInCentre = false;

            OnPlayerInCentreArea?.Invoke(false);
        }
    }

    private void Update()
    {
        if (!playerInCentre)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            TriggerFinalMoment();
        }
    }

    private void TriggerFinalMoment()
    {
        if (FinalMomentStarted)
            return;

        Debug.Log("Final Moment Triggered");

        FinalMomentStarted = true;
        ChangeInstructionText?.Invoke("");

        OnFinalMoment?.Invoke();
    }
}