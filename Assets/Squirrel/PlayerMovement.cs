using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash / Roll")]
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.6f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.5f;

    private Player player;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastNonZeroDir = Vector2.right;

    private bool isDashing = false;
    private bool canDash = true;

    public Vector2 MoveDir => moveInput.sqrMagnitude > 0.0001f ? moveInput.normalized : lastNonZeroDir;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!context.performed || !canDash) return;

        Vector2 dir = moveInput.sqrMagnitude > 0.0001f ? moveInput.normalized : lastNonZeroDir;
        StartCoroutine(DashRoutine(dir));
    }

    private IEnumerator DashRoutine(Vector2 dir)
    {
        canDash = false;
        isDashing = true;

        float originalDrag = rb.linearDamping;
        rb.linearDamping = 0f;

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = dir * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.linearDamping = originalDrag;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.0001f)
            lastNonZeroDir = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        rb.linearVelocity = moveInput * moveSpeed;
    }

}
