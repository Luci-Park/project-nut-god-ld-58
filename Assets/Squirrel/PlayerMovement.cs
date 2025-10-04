using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash / Roll")]
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.6f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastNonZeroDir = Vector2.right;

    private bool isDashing = false;
    private bool canDash = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Make dash crisp (prevent damping)
        float originalDrag = rb.linearDamping;
        rb.linearDamping = 0f;

        // Optional: clear momentum, then set instant dash velocity
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = dir * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.linearDamping = originalDrag;
        rb.linearVelocity = Vector2.zero; // uncomment if you want a hard stop after dash

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
