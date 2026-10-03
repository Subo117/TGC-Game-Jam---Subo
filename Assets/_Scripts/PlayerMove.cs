using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    Rigidbody2D rb;

    private Animator animator;
    bool isWalking;

    PlayerInput playerInput;
    Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();

        playerInput.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerInput.Player.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 input = movementInput;

        rb.linearVelocity = input.normalized * moveSpeed;

        isWalking = input.sqrMagnitude > 0.01f;
        animator.SetBool("IsWalking", isWalking);

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if (isWalking)
        {
            animator.SetFloat("LastInputX", input.x);
            animator.SetFloat("LastInputY", input.y);
        }


    }

}
