using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool canMove = true;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInput playerInput;

    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerInput = new PlayerInput();

        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        CentreArea.OnFinalMoment += StopMovement;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        CentreArea.OnFinalMoment -= StopMovement;
    }

    private void OnDestroy()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMoveCanceled;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.linearVelocity = movementInput.normalized * moveSpeed;

        bool isWalking = movementInput.sqrMagnitude > 0.01f;

        animator.SetBool("IsWalking", isWalking);

        animator.SetFloat("InputX", movementInput.x);
        animator.SetFloat("InputY", movementInput.y);

        if (isWalking)
        {
            animator.SetFloat("LastInputX", movementInput.x);
            animator.SetFloat("LastInputY", movementInput.y);
        }
    }

    private void StopMovement()
    {
        canMove = false;
        movementInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("IsWalking", false);
    }
}